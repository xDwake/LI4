namespace EletroExpo.Data
{
    public class Vendedor
    { 
        public String id {get; set; }

        public String nomeLoja { get; set; }

        public String email{ get; set; }

        public String password { get; set; }

        public bool isActive { get; set; }

        public Vendedor() { 
            
        }

        public Vendedor (string id,string nomeLoja, string email, string password, bool isActive)
        {
            this.id = id;
            this.email = email;
            this.password = password;
            this.nomeLoja = nomeLoja;
            this.isActive = isActive;
        }

    }
}
