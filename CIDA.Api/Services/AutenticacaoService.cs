﻿using System.Security.Cryptography;
using System.Text;
using CIDA.Api.Models;
using CIDA.Domain.Entities;

namespace CIDA.Api.Services;

public static class AutenticacaoService
{
    public static string QuickHash(string input)
    {
        var inputBytes = Encoding.UTF8.GetBytes(input);
        var inputHash = SHA256.HashData(inputBytes);
        return Convert.ToHexString(inputHash);
    }

    public static Autenticacao MapToAutenticacao(this UsuarioAndAutenticacaoAddOrUpdateModel model)
    {
        return new Autenticacao()
        {
            Email = model.Email, HashSenha = QuickHash(model.Senha)
        };
    }
}