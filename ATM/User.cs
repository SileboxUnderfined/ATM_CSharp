namespace ATM
{
    public class User
    {

        public string first_name { get; set; }

        public string last_name { get; set; }

        public long card_id { get; set; }

        public int balance { get; set; }

        public string filename { get; set; }

        public User(string first_name, string last_name, long card_id, int balance)
        {
            this.first_name = first_name;
            this.last_name = last_name;
            this.card_id = card_id;
            this.balance = balance;
        }
    }
}

