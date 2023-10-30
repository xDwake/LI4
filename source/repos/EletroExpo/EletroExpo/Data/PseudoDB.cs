using EletroExpo.Pages;

namespace EletroExpo.Data
{
    public class PseudoDB
    {
        public List<Vendedor> lojasDB;

        public PseudoDB()
        { 
            lojasDB = new List<Vendedor>();

            lojasDB.Add(new Vendedor("1","Gabriel","gabriel@gmail.com","minhapass",true));
            lojasDB.Add(new Vendedor("2", "Pedro", "pedro@gmail.com", "passP", true));
            lojasDB.Add(new Vendedor("3", "Miguel", "miguel@gmail.com", "passM", true));
            lojasDB.Add(new Vendedor("4", "Joao", "joao@gmail.com", "passJ", true));
    

        }

        public List<Vendedor> GetLojas()
        {
            return lojasDB;
        }

        public List<>
    }
}
