using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HojeEuCaso.Services
{
    public class FornecedorService : IFornecedorService
    {
        private readonly HojeEuCasoDbContext _HojeEuCasoDbContext;

        public FornecedorService(HojeEuCasoDbContext HojeEuCasoDbContext)
        {
            _HojeEuCasoDbContext = HojeEuCasoDbContext;
        }

        public List<Fornecedor> GetAllFornecedor()
        {
            return _HojeEuCasoDbContext.Fornecedores
                .Include(x => x.Cidade)
                .Include(x => x.Estado)
                .Include(x => x.Pais)
                .Include(x => x.Categoria)
                .Include(x => x.Plano)
                .ToList();
        }

        public Fornecedor GetFornecedorById(int ID)
        {
            return _HojeEuCasoDbContext.Fornecedores
                .Include(x => x.Cidade)
                .Include(x => x.Estado)
                .Include(x => x.Pais)
                .Include(x => x.Categoria)
                .Include(x => x.Plano)
                .Where(x => x.FornecedorID == ID)
                .FirstOrDefault();
        }

        public Fornecedor GetFornecedorByEmail(string email)
        {
            return _HojeEuCasoDbContext.Fornecedores
                .Include(x => x.Cidade)
                .Include(x => x.Estado)
                .Include(x => x.Pais)
                .Include(x => x.Categoria)
                .Include(x => x.Plano)
                .Where(x => x.Email == email)
                .FirstOrDefault();
        }

        public Fornecedor GetFornecedorLogin(UsuarioSistema usuarioSistema)
        {
            return _HojeEuCasoDbContext.Fornecedores
                .Where(x => x.Email == usuarioSistema.Email && x.Senha == usuarioSistema.Senha)
                .FirstOrDefault();
        }

        public void CreateNewFornecedor(Fornecedor Fornecedor)
        {
            try
            {
                _HojeEuCasoDbContext.Fornecedores.Add(Fornecedor);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdateFornecedor(Fornecedor Fornecedor)
        {
            try
            {
                _HojeEuCasoDbContext.Fornecedores.Update(Fornecedor);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeleteFornecedor(int ID)
        {
            try
            {
                var Fornecedor = GetFornecedorById(ID);
                _HojeEuCasoDbContext.Fornecedores.Remove(Fornecedor);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
