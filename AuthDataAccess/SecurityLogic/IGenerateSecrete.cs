
namespace AuthDataAccess.SecurityLogic
{
    public interface IGenerateSecrete
    {
        Task<IEnumerable<GenerateSecrete.SecreteModel>> getHashSeCrete();
        Task InsertSecreteToSystem(GenerateSecrete.SecreteModel secreteModel);
        Task CreateSecrete();
    }
}