using EFCore.Domain;
using EFCore.Obejct.EFCore.ValuesObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EFCore
{
    class Program
    {
        private static int clienteid;

        //metodos de execucoes dos metodos criados
        static void Main(string[] args)
        {
            InserirDadosEmMassa();
            ConsultarDados();
            CadastrarPedido();
            ExcluirDados();
        }


        //metodo p/ consultar registro
        public static void ConsultarDados()
        {
            //criando instancia do contexto
            using var db = new Data.ApplicationContext();

            //consulta utilizando Linq
            var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
            foreach (var Clientes in consultaPorSintaxe)
            {
                db.Clientes.Find(Clientes.Id);
            }

            //consulta utilizando metodo query
           //var consultaPorSintaxe = db.Clientes.Where(p => p.Id > 0).ToList();
        }


        //metodo p/ inserir registro
        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "123456789",
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true

            };

            //criando instancia do contexto
            using var db = new Data.ApplicationContext();
            //db.Produtos.Add(produto);
            db.Set<Produto>().Add(produto);
            //db.Entity(produto).State = EntityState.Added;
            //db.Add(produto);

            var registro = db.SaveChanges();

            Console.WriteLine($"Total registro");
        }


        //metodo p/ inserir registro em massa
        public static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "123456789",
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true

            };

            var cliente = new Cliente
            {
                Nome = "Alfredo Gomes",
                CEP = "31990055",
                Cidade = "Belo Horizonte",
                Estado = "MG",
                Telefone = "31995358198"
            };

            //criando instancia do contexto
            using var db = new Data.ApplicationContext();

            //db.AddRange(produto, cliente); 
            var registros = db.SaveChanges();
            Console.WriteLine($"Total Registro(s): {registros}");
        }


        //metodo p/ consultar registro adiatado
        private static void ConsultarPedidoCarregamentoAdiantado()
        {
            //criando instancia do contexto
            using var db = new Data.ApplicationContext();

            //chamando o metodo to list
            var pedidos = db.Pedidos.Include("Intes").ToList();

            //retornado todo registro em memoria
            Console.WriteLine(pedidos.Count);
        }


        //metodo p/ cadastrar registro 
        public static void CadastrarPedido()
        {
            //criando instancia do contexto
            using var db = new Data.ApplicationContext();

            //consulta 
            var Cliente = db.Clientes.FirstOrDefault();
            var Produto = db.Produtos.FirstOrDefault();

            //criando um pedido
            var pedido = new Pedido
            {
                //preechendo a propriedade do pedido
                ClienteId = clienteid,
                IniciadoEm = DateTime.Now,
                FinalizadoEM = DateTime.Now,
                Observacao = "Pedido Test",
                Status = Obejct.StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
            };

        }


        //metodo p/ atualizar registro
        public static void AtualizarDados()
        {
            //criando instancia do contexto
            using var db = new Data.ApplicationContext();

            //consultando informacoes
            var Cliente = db.Clientes.Find(1);
            //alterar o nome do cliente

            Cliente.Nome = "Cliente alterado Passo 1";
            //instacia do contexto
            db.Clientes.Update(Cliente);
          //db.Entry(cliente).State = EntityState.Modified;
            
            db.SaveChanges();

        }

        //metodo p/ atualizar registro
        public static void ExcluirDados()
        {
            //criando instancia do contexto
            using var db = new Data.ApplicationContext();

            //consultando informacoes
            var Cliente = db.Clientes.Find(2);
            //alterar o nome do cliente

            //Removendo instacia do contexto
            db.Clientes.Remove(Cliente);

            db.SaveChanges();
        }

    }
}