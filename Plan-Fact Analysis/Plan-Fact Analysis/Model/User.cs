using System;
using System.Security.Cryptography;
using System.Text;

namespace PlanFactAnalysis.Model
{
    public sealed class User
    {
        readonly string _login;
        readonly string _passHashed;
        readonly string _hashSalt;

        public string Name { get; set; }
        public string Login => _login;
        public UserRole Role { get; set; }

        public User (string name, string login, string passHashed, string hashSalt, UserRole role)
        {
            Name = name;
            _login = login;
            _passHashed = passHashed;
            _hashSalt = hashSalt;
            Role = role;
        }

        public User (string name, string login, string password, UserRole role)
        {
            Name = name;
            _login = login;

            #region Генерация «соли»
            StringBuilder saltBuilder = new StringBuilder ( );
            Random random = new Random ( );

            for (int i = 0; i < 8; i++)
                saltBuilder.Append (Convert.ToChar (Convert.ToInt32 (Math.Floor (26 * random.NextDouble ( ) + 65))));

            _hashSalt = saltBuilder.ToString ( );

            _passHashed = EncryptPassword (password, _hashSalt);
            #endregion

            Role = role;
        }

        string EncryptPassword (string password, string salt)
        {
            byte[ ] passHashedBytes = Encoding.Unicode.GetBytes (string.Concat (password, _hashSalt));
            MD5CryptoServiceProvider MD5provider = new MD5CryptoServiceProvider ( );
            passHashedBytes = MD5provider.ComputeHash (passHashedBytes);

            StringBuilder passHashedBuilder = new StringBuilder ( );

            for (int i = 0; i < passHashedBytes.Length; i++)
                passHashedBuilder.Append (passHashedBytes[i].ToString ("x2"));

            return passHashedBuilder.ToString ( );
        }

        public bool Verify (string password)
        {
            return _passHashed == EncryptPassword (password, _hashSalt);
        }

        public string GenerateSQLInsertQuery ( )
        {
            return string.Format (@"INSERT INTO user (login, password_hash, salt, name, role_id)
                 VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')", Login, _passHashed, _hashSalt, Name, (int)Role);
        }
    }
}