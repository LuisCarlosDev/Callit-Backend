﻿namespace Callit.Domain.Security.BCrypt;

public interface IPasswordEncripter
{
  string Encrypt(string password);
  bool Verify(string password, string passwordHash);
}
