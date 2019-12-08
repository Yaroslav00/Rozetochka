namespace Rozetochka
{
    public delegate void UsernameChangedDel(string str);


    static class SessionData
    {


        public static event UsernameChangedDel UsernameChangedEvent;

        private static string _username;
        public static string Username
        {
            get
            {
                return _username;
            }
            set
            {
                if (value == "") _username = null;
                else _username = value;

                UsernameChangedEvent(_username);
            }
        }

        public static int ID { get; set; }
        public static bool IsAdmin { get; set; }
        public static string Password { get; set; }
    }
}
