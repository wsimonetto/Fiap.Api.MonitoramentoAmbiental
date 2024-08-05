using Fiap.Api.MonitoramentoAmbiental.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Fiap.Api.MonitoramentoAmbiental.Data
{
    
        public class DatabaseContext : DbContext
        {
            public virtual DbSet<SensorModel> Sensores { get; set; }
            public virtual DbSet<LeituraModel> Leituras { get; set; }
            public virtual DbSet<PrevisaoChuvaModel> PrevisoesChuva { get; set; }
            public virtual DbSet<ControleIrrigacaoModel> ControleIrrigacoes { get; set; }
            public virtual DbSet<AlertaModel> Alertas { get; set; }

            public DatabaseContext(DbContextOptions options) : base(options)
            {
            }

            protected DatabaseContext()
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<SensorModel>(entity =>
                {
                    entity.ToTable("Sensores"); // Define o nome da tabela no banco de dados

                    entity.HasKey(e => e.SensorId); // Define SensorId como chave primária

                    entity.Property(e => e.SensorId)
                        .HasColumnName("id_sensor"); // Mapeia SensorId para a coluna "id_sensor"

                    entity.Property(e => e.TipoSensor)
                        .HasColumnName("tipo_sensor").IsRequired(); // Mapeia TipoSensor para a coluna "tipo_sensor"

                    entity.Property(e => e.Localizacao)
                        .HasColumnName("localizacao").IsRequired(); // Mapeia Localizacao para a coluna "localizacao"

                    // Configura o relacionamento com LeituraModel
                    entity.HasMany(e => e.Leitura)
                          .WithOne(leitura => leitura.Sensor)
                          .HasForeignKey(leitura => leitura.SensorId);
                });

                modelBuilder.Entity<LeituraModel>(entity =>
                {
                    entity.ToTable("Leituras"); // Define o nome da tabela no banco de dados

                    entity.HasKey(e => e.LeituraId); // Define LeituraId como chave primária

                    entity.Property(e => e.LeituraId)
                        .HasColumnName("id_leitura"); // Mapeia LeituraId para a coluna "id_leitura"

                    entity.Property(e => e.Valor)
                        .HasColumnName("valor")
                        .HasColumnType("float")
                        .IsRequired(); // Mapeia Valor para a coluna "valor"

                    entity.Property(e => e.DataHora)
                        .HasColumnName("data_hora")
                        .HasColumnType("date")
                        .IsRequired(); // Mapeia DataHora para a coluna "data_hora"

                    entity.Property(e => e.SensorId)
                        .HasColumnName("id_sensor"); // Mapeia SensorId para a coluna "id_sensor"
                });

                modelBuilder.Entity<PrevisaoChuvaModel>(entity =>
                {
                    entity.ToTable("PrevisoesChuva"); // Define o nome da tabela no banco de dados

                    entity.HasKey(e => e.PrevisaoChuvaId); // Define PrevisaoChuvaId como chave primária

                    entity.Property(e => e.PrevisaoChuvaId)
                        .HasColumnName("id_previsao_chuva"); // Mapeia PrevisaoChuvaId para a coluna "id_previsao_chuva"

                    entity.Property(e => e.DataPrevisaoChuva)
                        .HasColumnName("data_previsao")
                        .HasColumnType("date")
                        .IsRequired(); // Mapeia DataPrevisaoChuva para a coluna "data_previsao"

                    entity.Property(e => e.Previsao)
                        .HasColumnName("previsao")
                        .IsRequired(); // Mapeia Previsao para a coluna "previsao"
                });

                modelBuilder.Entity<ControleIrrigacaoModel>(entity =>
                {
                    entity.ToTable("ControleIrrigacoes"); // Define o nome da tabela no banco de dados

                    entity.HasKey(e => e.ControleId); // Define ControleId como chave primária

                    entity.Property(e => e.ControleId)
                        .HasColumnName("id_controle"); // Mapeia ControleId para a coluna "id_controle"

                    entity.Property(e => e.Localizacao)
                        .HasColumnName("localizacao")
                        .IsRequired(); // Mapeia Localizacao para a coluna "localizacao" e define como obrigatória

                    entity.Property(e => e.Estado)
                        .HasColumnName("estado")
                        .IsRequired(); // Mapeia Estado para a coluna "estado" e define como obrigatório

                    entity.Property(e => e.DataHora)
                        .HasColumnName("data_hora")
                        .HasColumnType("date") // Define o tipo de coluna como datetime
                        .IsRequired(); // Mapeia DataHora para a coluna "data_hora" e define como obrigatória

                    entity.Property(e => e.PrevisaoChuvaId)
                        .HasColumnName("id_previsao_chuva"); // Mapeia PrevisaoChuvaId para a coluna "id_previsao_chuva"

                    // Configura o relacionamento com PrevisaoChuvaModel
                    entity.HasOne(e => e.PrevisaoChuva)
                          .WithMany()
                          .HasForeignKey(e => e.PrevisaoChuvaId)
                          .IsRequired(); // Define o relacionamento como obrigatório
                });


                modelBuilder.Entity<AlertaModel>(entity =>
                {
                    entity.ToTable("Alertas"); // Define o nome da tabela no banco de dados

                    entity.HasKey(e => e.AlertaId); // Define AlertaId como chave primária

                    entity.Property(e => e.AlertaId)
                        .HasColumnName("id_alerta"); // Mapeia AlertaId para a coluna "id_alerta"

                    entity.Property(e => e.TipoAlerta)
                        .HasColumnName("tipo_alerta")
                        .IsRequired(); // Mapeia TipoAlerta para a coluna "tipo_alerta"

                    entity.Property(e => e.Descricao)
                        .HasColumnName("descricao")
                        .IsRequired(); // Mapeia Descricao para a coluna "descricao"

                    entity.Property(e => e.DataHora)
                        .HasColumnName("data_hora")
                        .HasColumnType("date")
                        .IsRequired(); // Mapeia DataHora para a coluna "data_hora"
                });

                base.OnModelCreating(modelBuilder);
            }
        }
    
}
