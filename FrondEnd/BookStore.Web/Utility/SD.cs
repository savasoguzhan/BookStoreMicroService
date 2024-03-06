namespace BookStore.Web.Utility
{
    public class SD
    {

        public static string DiscountAPIBase { get; set;}
		public static string AuthAPIBase { get; set; }

        public static string BookAPIBase { get; set; } 
        public static string CartAPIBase { get; set; } 

        public const string RoleAdmin = "ADMIN";
        public const string RoleUser = "USER";

        public const string TokenCookie = "JWTToken";




		public enum ApiType
        {
            GET, POST, PUT, DELETE
        }
    }
}
