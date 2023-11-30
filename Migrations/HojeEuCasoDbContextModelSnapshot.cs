﻿// <auto-generated />
using System;
using HojeEuCaso.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HojeEuCaso.Migrations
{
    [DbContext(typeof(HojeEuCasoDbContext))]
    partial class HojeEuCasoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("HojeEuCaso.Models.Agenda", b =>
                {
                    b.Property<int>("AgendaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataFinal")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataInicial")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UsuarioID")
                        .HasColumnType("int");

                    b.HasKey("AgendaID");

                    b.HasIndex("UsuarioID");

                    b.ToTable("Agendas");
                });

            modelBuilder.Entity("HojeEuCaso.Models.Categoria", b =>
                {
                    b.Property<int>("CategoriaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext");

                    b.Property<int>("TipoCategoriaID")
                        .HasColumnType("int");

                    b.HasKey("CategoriaID");

                    b.HasIndex("TipoCategoriaID");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("HojeEuCaso.Models.CategoriaDosPlanos", b =>
                {
                    b.Property<int>("CategoriaDosPlanosID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoriaID")
                        .HasColumnType("int");

                    b.Property<int>("PlanoID")
                        .HasColumnType("int");

                    b.HasKey("CategoriaDosPlanosID");

                    b.HasIndex("CategoriaID");

                    b.HasIndex("PlanoID");

                    b.ToTable("CategoriasDosPlanos");
                });

            modelBuilder.Entity("HojeEuCaso.Models.Cidade", b =>
                {
                    b.Property<int>("CidadeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.HasKey("CidadeID");

                    b.ToTable("Cidades");
                });

            modelBuilder.Entity("HojeEuCaso.Models.ClausulaContrato", b =>
                {
                    b.Property<int>("ClausulaContratoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoriaID")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext");

                    b.Property<int?>("FornecedorID")
                        .HasColumnType("int");

                    b.HasKey("ClausulaContratoID");

                    b.HasIndex("CategoriaID");

                    b.HasIndex("FornecedorID");

                    b.ToTable("ClausulasContratos");
                });

            modelBuilder.Entity("HojeEuCaso.Models.Estado", b =>
                {
                    b.Property<int>("EstadoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<string>("UF")
                        .HasColumnType("longtext");

                    b.HasKey("EstadoID");

                    b.ToTable("Estados");
                });

            modelBuilder.Entity("HojeEuCaso.Models.Fornecedor", b =>
                {
                    b.Property<int>("FornecedorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("AceitaMultiplosEventos")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Agencia")
                        .HasColumnType("longtext");

                    b.Property<string>("Avaliacao")
                        .HasColumnType("longtext");

                    b.Property<string>("Bairro")
                        .HasColumnType("longtext");

                    b.Property<string>("Banco")
                        .HasColumnType("longtext");

                    b.Property<string>("CEP")
                        .HasColumnType("longtext");

                    b.Property<int>("CNPJ")
                        .HasColumnType("int");

                    b.Property<string>("CPF")
                        .HasColumnType("longtext");

                    b.Property<string>("CPFCNPJResponsavelConta")
                        .HasColumnType("longtext");

                    b.Property<string>("CaminhoFoto")
                        .HasColumnType("longtext");

                    b.Property<int>("CategoriaID")
                        .HasColumnType("int");

                    b.Property<int>("CidadeID")
                        .HasColumnType("int");

                    b.Property<decimal>("DisponivelParaRecebimento")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Endereco")
                        .HasColumnType("longtext");

                    b.Property<int>("EstadoID")
                        .HasColumnType("int");

                    b.Property<string>("Facebook")
                        .HasColumnType("longtext");

                    b.Property<int>("IDIdentificacao")
                        .HasColumnType("int");

                    b.Property<string>("InscricaoEstadual")
                        .HasColumnType("longtext");

                    b.Property<string>("Instagram")
                        .HasColumnType("longtext");

                    b.Property<string>("MotivoExclusao")
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<string>("NomeFantasia")
                        .HasColumnType("longtext");

                    b.Property<string>("NomeResponsavel")
                        .HasColumnType("longtext");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<string>("NumeroConta")
                        .HasColumnType("longtext");

                    b.Property<int?>("PaisID")
                        .HasColumnType("int");

                    b.Property<int?>("PlanoID")
                        .HasColumnType("int");

                    b.Property<string>("RG")
                        .HasColumnType("longtext");

                    b.Property<string>("RazaoSocial")
                        .HasColumnType("longtext");

                    b.Property<string>("ResponsavelConta")
                        .HasColumnType("longtext");

                    b.Property<string>("Senha")
                        .HasColumnType("longtext");

                    b.Property<string>("Site")
                        .HasColumnType("longtext");

                    b.Property<bool>("SolicitouExclusao")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.Property<string>("Telefone")
                        .HasColumnType("longtext");

                    b.Property<int>("TelefoneResponsavel")
                        .HasColumnType("int");

                    b.Property<string>("Tipo")
                        .HasColumnType("longtext");

                    b.Property<string>("TipoConta")
                        .HasColumnType("longtext");

                    b.Property<decimal>("TotalAReceber")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("TotalRecebido")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("FornecedorID");

                    b.HasIndex("CategoriaID");

                    b.HasIndex("CidadeID");

                    b.HasIndex("EstadoID");

                    b.HasIndex("PaisID");

                    b.HasIndex("PlanoID");

                    b.ToTable("Fornecedores");
                });

            modelBuilder.Entity("HojeEuCaso.Models.FornecedorIndicado", b =>
                {
                    b.Property<int>("FornecedorIndicadoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("FornecedorID")
                        .HasColumnType("int");

                    b.Property<string>("NomeFornecedor")
                        .HasColumnType("longtext");

                    b.Property<decimal>("TotalAReceber")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("FornecedorIndicadoID");

                    b.HasIndex("FornecedorID");

                    b.ToTable("FornecedoresIndicados");
                });

            modelBuilder.Entity("HojeEuCaso.Models.FotosServicos", b =>
                {
                    b.Property<int>("FotosServicosID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CaminhoFoto")
                        .HasColumnType("longtext");

                    b.Property<int>("PacoteID")
                        .HasColumnType("int");

                    b.HasKey("FotosServicosID");

                    b.HasIndex("PacoteID");

                    b.ToTable("FotosServicos");
                });

            modelBuilder.Entity("HojeEuCaso.Models.ItensDePacotes", b =>
                {
                    b.Property<int>("ItensDePacotesID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext");

                    b.Property<int>("PacoteID")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("ItensDePacotesID");

                    b.HasIndex("PacoteID");

                    b.ToTable("ItensDePacotes");
                });

            modelBuilder.Entity("HojeEuCaso.Models.ItensDePacotesDeUsuarios", b =>
                {
                    b.Property<int>("ItensDePacotesDeUsuariosID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext");

                    b.Property<int>("PacoteID")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("ItensDePacotesDeUsuariosID");

                    b.HasIndex("PacoteID");

                    b.ToTable("ItensDePacotesDeUsuarios");
                });

            modelBuilder.Entity("HojeEuCaso.Models.Pacote", b =>
                {
                    b.Property<int>("PacoteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Ativo")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("CaminhoFoto")
                        .HasColumnType("longtext");

                    b.Property<string>("CaminhoVideo")
                        .HasColumnType("longtext");

                    b.Property<int>("CategoriaID")
                        .HasColumnType("int");

                    b.Property<int?>("CidadeID")
                        .HasColumnType("int");

                    b.Property<decimal>("DescontoDomingo")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("DescontoQuartaFeira")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("DescontoQuintaFeira")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("DescontoSabado")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("DescontoSegundaFeira")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("DescontoSextaFeira")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("DescontoTercaFeira")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("EstadoID")
                        .HasColumnType("int");

                    b.Property<int?>("FornecedorID")
                        .HasColumnType("int");

                    b.Property<int?>("PaisID")
                        .HasColumnType("int");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("QtdMaximaEventosDia")
                        .HasColumnType("int");

                    b.Property<int>("QtdMaximaPessoas")
                        .HasColumnType("int");

                    b.Property<decimal>("ReajusteAnualPorcentagem")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("SubTitulo")
                        .HasColumnType("longtext");

                    b.Property<string>("Titulo")
                        .HasColumnType("longtext");

                    b.HasKey("PacoteID");

                    b.HasIndex("CategoriaID");

                    b.HasIndex("CidadeID");

                    b.HasIndex("EstadoID");

                    b.HasIndex("FornecedorID");

                    b.HasIndex("PaisID");

                    b.ToTable("Pacotes");
                });

            modelBuilder.Entity("HojeEuCaso.Models.PacotesDeUsuarios", b =>
                {
                    b.Property<int>("PacotesDeUsuariosID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("FornecedorID")
                        .HasColumnType("int");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("QtdMaximaDeEventosDia")
                        .HasColumnType("int");

                    b.Property<int>("QtdMaximaDePessoas")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .HasColumnType("longtext");

                    b.HasKey("PacotesDeUsuariosID");

                    b.HasIndex("FornecedorID");

                    b.ToTable("PacotesDeUsuarios");
                });

            modelBuilder.Entity("HojeEuCaso.Models.Pais", b =>
                {
                    b.Property<int>("PaisID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.HasKey("PaisID");

                    b.ToTable("Paises");
                });

            modelBuilder.Entity("HojeEuCaso.Models.Plano", b =>
                {
                    b.Property<int>("PlanoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("PeriodoRenovacao")
                        .HasColumnType("longtext");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Titulo")
                        .HasColumnType("longtext");

                    b.HasKey("PlanoID");

                    b.ToTable("Planos");
                });

            modelBuilder.Entity("HojeEuCaso.Models.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Ativo")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("HojeEuCaso.Models.TipoCategoria", b =>
                {
                    b.Property<int>("TipoCategoriaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext");

                    b.HasKey("TipoCategoriaID");

                    b.ToTable("TiposCategorias");
                });

            modelBuilder.Entity("HojeEuCaso.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Ativo")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("CNPJ")
                        .HasColumnType("longtext");

                    b.Property<string>("CPF")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<string>("Senha")
                        .HasColumnType("longtext");

                    b.HasKey("UsuarioID");

                    b.HasIndex("RoleID");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("HojeEuCaso.Models.UsuarioSistema", b =>
                {
                    b.Property<int>("UsuarioSistemaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Avaliacao")
                        .HasColumnType("longtext");

                    b.Property<string>("Bairro")
                        .HasColumnType("longtext");

                    b.Property<int>("CEP")
                        .HasColumnType("int");

                    b.Property<int>("CPF")
                        .HasColumnType("int");

                    b.Property<int>("CidadeID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Endereco")
                        .HasColumnType("longtext");

                    b.Property<int>("EstadoID")
                        .HasColumnType("int");

                    b.Property<int>("IDIdentificacao")
                        .HasColumnType("int");

                    b.Property<string>("MotivoExclusao")
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<int?>("PaisID")
                        .HasColumnType("int");

                    b.Property<int>("RG")
                        .HasColumnType("int");

                    b.Property<string>("Senha")
                        .HasColumnType("longtext");

                    b.Property<bool>("SolicitouExclusao")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Telefone")
                        .HasColumnType("int");

                    b.Property<string>("Tipo")
                        .HasColumnType("longtext");

                    b.HasKey("UsuarioSistemaID");

                    b.HasIndex("CidadeID");

                    b.HasIndex("EstadoID");

                    b.HasIndex("PaisID");

                    b.ToTable("UsuariosSistema");
                });

            modelBuilder.Entity("HojeEuCaso.Models.Agenda", b =>
                {
                    b.HasOne("HojeEuCaso.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("HojeEuCaso.Models.Categoria", b =>
                {
                    b.HasOne("HojeEuCaso.Models.TipoCategoria", "TipoCategoria")
                        .WithMany()
                        .HasForeignKey("TipoCategoriaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoCategoria");
                });

            modelBuilder.Entity("HojeEuCaso.Models.CategoriaDosPlanos", b =>
                {
                    b.HasOne("HojeEuCaso.Models.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HojeEuCaso.Models.Plano", "Plano")
                        .WithMany()
                        .HasForeignKey("PlanoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Plano");
                });

            modelBuilder.Entity("HojeEuCaso.Models.ClausulaContrato", b =>
                {
                    b.HasOne("HojeEuCaso.Models.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HojeEuCaso.Models.Fornecedor", "Fornecedor")
                        .WithMany("ClausulasContrato")
                        .HasForeignKey("FornecedorID");

                    b.Navigation("Categoria");

                    b.Navigation("Fornecedor");
                });

            modelBuilder.Entity("HojeEuCaso.Models.Fornecedor", b =>
                {
                    b.HasOne("HojeEuCaso.Models.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HojeEuCaso.Models.Cidade", "Cidade")
                        .WithMany()
                        .HasForeignKey("CidadeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HojeEuCaso.Models.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HojeEuCaso.Models.Pais", "Pais")
                        .WithMany()
                        .HasForeignKey("PaisID");

                    b.HasOne("HojeEuCaso.Models.Plano", "Plano")
                        .WithMany()
                        .HasForeignKey("PlanoID");

                    b.Navigation("Categoria");

                    b.Navigation("Cidade");

                    b.Navigation("Estado");

                    b.Navigation("Pais");

                    b.Navigation("Plano");
                });

            modelBuilder.Entity("HojeEuCaso.Models.FornecedorIndicado", b =>
                {
                    b.HasOne("HojeEuCaso.Models.Fornecedor", "FornecedorPai")
                        .WithMany()
                        .HasForeignKey("FornecedorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FornecedorPai");
                });

            modelBuilder.Entity("HojeEuCaso.Models.FotosServicos", b =>
                {
                    b.HasOne("HojeEuCaso.Models.Pacote", "Pacote")
                        .WithMany()
                        .HasForeignKey("PacoteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pacote");
                });

            modelBuilder.Entity("HojeEuCaso.Models.ItensDePacotes", b =>
                {
                    b.HasOne("HojeEuCaso.Models.Pacote", "Pacote")
                        .WithMany()
                        .HasForeignKey("PacoteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pacote");
                });

            modelBuilder.Entity("HojeEuCaso.Models.ItensDePacotesDeUsuarios", b =>
                {
                    b.HasOne("HojeEuCaso.Models.Pacote", "Pacote")
                        .WithMany()
                        .HasForeignKey("PacoteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pacote");
                });

            modelBuilder.Entity("HojeEuCaso.Models.Pacote", b =>
                {
                    b.HasOne("HojeEuCaso.Models.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HojeEuCaso.Models.Cidade", "Cidade")
                        .WithMany()
                        .HasForeignKey("CidadeID");

                    b.HasOne("HojeEuCaso.Models.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoID");

                    b.HasOne("HojeEuCaso.Models.Fornecedor", "Fornecedor")
                        .WithMany()
                        .HasForeignKey("FornecedorID");

                    b.HasOne("HojeEuCaso.Models.Pais", "Pais")
                        .WithMany()
                        .HasForeignKey("PaisID");

                    b.Navigation("Categoria");

                    b.Navigation("Cidade");

                    b.Navigation("Estado");

                    b.Navigation("Fornecedor");

                    b.Navigation("Pais");
                });

            modelBuilder.Entity("HojeEuCaso.Models.PacotesDeUsuarios", b =>
                {
                    b.HasOne("HojeEuCaso.Models.Fornecedor", "Fornecedor")
                        .WithMany()
                        .HasForeignKey("FornecedorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fornecedor");
                });

            modelBuilder.Entity("HojeEuCaso.Models.Usuario", b =>
                {
                    b.HasOne("HojeEuCaso.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("HojeEuCaso.Models.UsuarioSistema", b =>
                {
                    b.HasOne("HojeEuCaso.Models.Cidade", "Cidade")
                        .WithMany()
                        .HasForeignKey("CidadeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HojeEuCaso.Models.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HojeEuCaso.Models.Pais", "Pais")
                        .WithMany()
                        .HasForeignKey("PaisID");

                    b.Navigation("Cidade");

                    b.Navigation("Estado");

                    b.Navigation("Pais");
                });

            modelBuilder.Entity("HojeEuCaso.Models.Fornecedor", b =>
                {
                    b.Navigation("ClausulasContrato");
                });
#pragma warning restore 612, 618
        }
    }
}
