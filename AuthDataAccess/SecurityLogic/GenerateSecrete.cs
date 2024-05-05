using AuthDataAccess.SqlAccess;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace AuthDataAccess.SecurityLogic
{
    public partial class GenerateSecrete : IGenerateSecrete
    {
        ISqlAccess _sqlAccess;

        private const string _Identity = "!Aphiwat33110!";
        public GenerateSecrete(ISqlAccess sqlAccess)
        {
            _sqlAccess = sqlAccess;
        }

        public async Task<IEnumerable<SecreteModel>> getHashSeCrete()
        {
            var result = await _sqlAccess.LoadAsync<SecreteModel, dynamic>(storedProcedure: "dbo.spGetSecrete", new { });

            return result;
        }

        public async Task InsertSecreteToSystem(SecreteModel secreteModel)
        {
            await _sqlAccess.UpdateAsync(storedProcedures: "dbo.spInsertSecrete", new
            {
                HashSecrete = secreteModel.HashSecrete,
                Salt = secreteModel.SaltSecrete,
                SecreteIdentity = secreteModel.SecreteIdentity
            });
        }


        public async Task CreateSecrete() {

            var result = EncryptSecrete.HashSecrete(_Identity);
            var secreteModel = new SecreteModel
            {
                HashSecrete = result.Hash,
                SaltSecrete = result.Salt,
                SecreteIdentity = _Identity
            };
            await InsertSecreteToSystem(secreteModel);

            
        }


        

    }
}
