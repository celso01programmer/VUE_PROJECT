
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProjectSchool_API.Models;

namespace ProjectSchool_API.Data
{

    //Contexto da aplicação (Gestao de Dados)
    public class DataContext : DbContext
    {

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Professor>()
                    .HasData(
                        new List<Professor>() {
                            new Professor(){
                                Id = 1,
                                Nome = "Vinicius",
                            },
                            new Professor(){
                                Id = 2,
                                Nome = "Paula",
                            },
                            new Professor(){
                                Id = 3,
                                Nome = "Luna",
                            },
                            
                        }
                    );

            builder.Entity<Aluno>()
                    .HasData(
                        new List<Aluno>() {
                            new Aluno() {
                                Id = 1,
                                Nome = "Maria",
                                Sobrenome = "Goncalves",
                                DataNasc = "20/01/1990",
                                ProfessorId = 1
                            },                            

                            new Aluno() {
                                Id = 2,
                                Nome = "João",
                                Sobrenome = "Paulo",
                                DataNasc = "23/12/1987",
                                ProfessorId = 2                                
                            },
                                                       
                            new Aluno() {
                                Id = 3,
                                Nome = "Alex",
                                Sobrenome = "Ferrari",
                                DataNasc = "17/07/2001",
                                ProfessorId = 3                               
                            }                            
                        }
                    );

        }
        
    }
}