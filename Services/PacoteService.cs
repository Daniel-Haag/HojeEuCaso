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
    public class PacoteService : IPacoteService
    {
        private readonly HojeEuCasoDbContext _HojeEuCasoDbContext;

        public PacoteService(HojeEuCasoDbContext HojeEuCasoDbContext)
        {
            _HojeEuCasoDbContext = HojeEuCasoDbContext;
        }

        public List<Pacote> GetAllPacotes()
        {
            return _HojeEuCasoDbContext.Pacotes
                .Include(x => x.Fornecedor)
                .Include(x => x.Categoria)
                .Include(x => x.Cidade)
                .Include(x => x.Estado)
                .Include(x => x.Pais)
                .AsNoTracking()
                .Where(x => x.Ativo == true)
                .ToList();
        }

        public List<Pacote> GetPacotesByCategoriaID(int categoriaID)
        {
            return _HojeEuCasoDbContext.Pacotes
                .Include(x => x.Fornecedor)
                .Include(x => x.Categoria)
                .Include(x => x.Cidade)
                .Include(x => x.Estado)
                .Include(x => x.Pais)
                .AsNoTracking()
                .Where(x => x.Categoria.CategoriaID == categoriaID
                && x.Ativo == true)
                .ToList();
        }

        public Pacote GetPacoteById(int ID)
        {
            return _HojeEuCasoDbContext.Pacotes
                .Include(x => x.Fornecedor)
                .Include(x => x.Categoria)
                .Include(x => x.Cidade)
                .Include(x => x.Estado)
                .Include(x => x.Pais)
                .AsNoTracking()
                .FirstOrDefault(x => x.PacoteID == ID
                && x.Ativo == true);
        }

        public Pacote GetPacoteByTitulo(string titulo)
        {
            return _HojeEuCasoDbContext.Pacotes
                .Include(x => x.Fornecedor)
                .Include(x => x.Categoria)
                .Include(x => x.Cidade)
                .Include(x => x.Estado)
                .Include(x => x.Pais)
                .AsNoTracking()
                .FirstOrDefault(x => x.Titulo == titulo
                && x.Ativo == true);
        }

        public List<Pacote> GetPacoteByFornecedor(int fornecedorID)
        {
            return _HojeEuCasoDbContext.Pacotes
                .Include(x => x.Fornecedor)
                .Include(x => x.Categoria)
                .Include(x => x.Cidade)
                .Include(x => x.Estado)
                .Include(x => x.Pais)
                .Where(x => x.FornecedorID == fornecedorID
                && x.Ativo == true)
                .AsNoTracking()
                .ToList();
        }

        public int CreateNewPacote(Pacote Pacote)
        {
            try
            {
                _HojeEuCasoDbContext.Pacotes.Add(Pacote);
                _HojeEuCasoDbContext.SaveChanges();

                return Pacote.PacoteID;
            }
            catch (Exception e)
            {
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdatePacote(Pacote Pacote)
        {
            try
            {
                _HojeEuCasoDbContext.Pacotes.Update(Pacote);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                string erro = e.Message;
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeletePacote(int ID)
        {
            try
            {
                var pacote = GetPacoteById(ID);
                if (pacote != null)
                {
                    _HojeEuCasoDbContext.Entry(pacote).State = EntityState.Detached;

                    pacote.Ativo = false;
                    _HojeEuCasoDbContext.Pacotes.Update(pacote);
                    _HojeEuCasoDbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
