using System;

namespace API.Entities
{
    public class Message //Relazione molti a molti.
    //Utente => invia tanti messaggi.
    //Utente => riceve tanti messaggi.
    {   
        public int Id { get; set; }
        //da qui inizano le proprietà che definiscono la relazione
        public int SenderId { get; set; }
        public string SenderUsername { get; set; }

        public AppUser Sender { get; set; }
         public int RecipientId { get; set; }
        public string RecipientUsername { get; set; }
        public AppUser Recipient { get; set; }
        //quindi per definire la relazione tra 2 appUser serve l'id degli user, e gli user stessi.
        
        //proprietà del messaggio stesso.
        public string Content { get; set; }

        public DateTime? DateRead { get; set; } //se null => non ancora letto messaggio.
        public DateTime MessageSent { get; set; } = DateTime.Now;
        public bool  SenderDeleted{ get; set; } //L'unico momento che eliminiamo un messaggio dal server,
        // è se entrambi lo hanno eliminato.
        public bool  RecipientDeleted { get; set; }
    }
}