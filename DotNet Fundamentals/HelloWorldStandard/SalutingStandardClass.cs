namespace HelloWorldStandard
{
    public class SalutingStandardClass
    {
        public string SaluteMe(string username)
        {
            return $"{DateTime.Now} Hello, {username.ToUpper()}";
        }
    }
}