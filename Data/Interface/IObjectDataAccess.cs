using GoAndSee_API.Models;
using GoAndSee_API.Models.DTO.Objects;
using System.Collections.Generic;

namespace GoAndSee_API.Data
{
    public interface IObjectDataAccess
    {
        ObjectDTO readObject(string id);
        List<ObjectListDTO> readAllObject();
        string updateObject(Object @object);
        void deleteObject(string id);
        void createObject(Object @object);
        void updateLastModified(string id);
    }
}
