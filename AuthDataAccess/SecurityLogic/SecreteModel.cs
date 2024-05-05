namespace AuthDataAccess.SecurityLogic
{
    public partial class GenerateSecrete
    {
        public class SecreteModel
        {
            public string HashSecrete { get; set; }
            public string SaltSecrete { get; set; }
            public string SecreteIdentity { get; set; }
        }

    }
}
