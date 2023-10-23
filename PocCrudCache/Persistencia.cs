using InterSystems.Data.CacheClient;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace PocCrudCache
{
    public class Persistencia
    {
        public void TestarAdo()
        {

            string stringDeConexao = $"Server=10.2.0.8;Port=1972;Namespace=classven;User=hugo.vettorello;Password=hoN@0312";
            CacheConnection connection = new CacheConnection();
            connection.ConnectionString = stringDeConexao;

            void showHeaderMessage()
            {
                Console.WriteLine(@"
    
░█████╗░░█████╗░███╗░░██╗░██████╗░█████╗░██╗░░░░░███████╗
██╔══██╗██╔══██╗████╗░██║██╔════╝██╔══██╗██║░░░░░██╔════╝
██║░░╚═╝██║░░██║██╔██╗██║╚█████╗░██║░░██║██║░░░░░█████╗░░
██║░░██╗██║░░██║██║╚████║░╚═══██╗██║░░██║██║░░░░░██╔══╝░░
╚█████╔╝╚█████╔╝██║░╚███║██████╔╝╚█████╔╝███████╗███████╗
░╚════╝░░╚════╝░╚═╝░░╚══╝╚═════╝░░╚════╝░╚══════╝╚══════╝

░█████╗░██████╗░██████╗░██╗░░░░░██╗░█████╗░░█████╗░████████╗██╗░█████╗░███╗░░██╗
██╔══██╗██╔══██╗██╔══██╗██║░░░░░██║██╔══██╗██╔══██╗╚══██╔══╝██║██╔══██╗████╗░██║
███████║██████╔╝██████╔╝██║░░░░░██║██║░░╚═╝███████║░░░██║░░░██║██║░░██║██╔██╗██║
██╔══██║██╔═══╝░██╔═══╝░██║░░░░░██║██║░░██╗██╔══██║░░░██║░░░██║██║░░██║██║╚████║
██║░░██║██║░░░░░██║░░░░░███████╗██║╚█████╔╝██║░░██║░░░██║░░░██║╚█████╔╝██║░╚███║
╚═╝░░╚═╝╚═╝░░░░░╚═╝░░░░░╚══════╝╚═╝░╚════╝░╚═╝░░╚═╝░░░╚═╝░░░╚═╝░╚════╝░╚═╝░░╚══╝
");

            };

            void showFooterMessage()
            {
                Console.WriteLine(@"
    
████████╗██╗░░██╗███████╗  ███████╗███╗░░██╗██████╗░
╚══██╔══╝██║░░██║██╔════╝  ██╔════╝████╗░██║██╔══██╗
░░░██║░░░███████║█████╗░░  █████╗░░██╔██╗██║██║░░██║
░░░██║░░░██╔══██║██╔══╝░░  ██╔══╝░░██║╚████║██║░░██║
░░░██║░░░██║░░██║███████╗  ███████╗██║░╚███║██████╔╝
░░░╚═╝░░░╚═╝░░╚═╝╚══════╝  ╚══════╝╚═╝░░╚══╝╚═════╝░
    ");


            };
            showHeaderMessage();

            try
            {
                // Abrindo a conexão
                connection.Open();

                CacheCommand command = new CacheCommand("SELECT Especie ,FATURA_NOTA_FISCAL ,CLIENTE ->rAZAOsOCIAL As RazaoSocial\r\nFROM SQLUser.FATURA_NOTA_FISCAL\r\nWHERE DataEmissao >'2021-01-01'AND NumeroNF is null \r\nORDER BY childsub ASC", connection);
                CacheDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Processar os dados do resultado
                    Console.WriteLine($"\nFATNF: {reader["FATURA_NOTA_FISCAL"]}, Especie: {reader["Especie"]}, Cliente: {reader["RazaoSocial"]}\n");
                }

            }
            catch (CacheException ex)
            {
                // Trate exceções relacionadas ao Cache
                Console.WriteLine($"Erro de Cache: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            showFooterMessage();
        }
        public void TestarEntityFramework()
        {
            try
            {
                using (var ctx = new SorteioContext())
                {
                    var sorteios = ctx.Sorteios.Skip(10).ToList();

                    foreach (var sorteio in sorteios)
                        Console.WriteLine($"Cupom {sorteio.CupomSorteado}");
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }

    public class TabelaSorteio
    {
        public int ID { get; set; }
        public int Cliente { get; set; }
        public string CupomSorteado { get; set; }
        public DateTime DataSorteio { get; set; }
        public string IdPedido { get; set; }
        public string Pedido { get; set; }
        public string Situacao { get; set; }
    }

    public class SorteioContext : DbContext
    {
        public SorteioContext() : base("name=legadoContext")
        {

        }

        public DbSet<TabelaSorteio> Sorteios { get; set; }
    }
}


