using GoAndSee_API.Models;
using System.Collections.Generic;

namespace GoAndSee_API.Data
{
    interface ISubobjectDataAccess
    {
        List<Subobject> readSubobjectbyOId(string id);
        List<Subobject> readAllSubobject();
        void deleteSubobject(string id);
        void createSubobject(Subobject sobject);
        string readSubobjectsBySId(string id);
        List<string> readAllSubobjectsOId(string id);
    }
}
