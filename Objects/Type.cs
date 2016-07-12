using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace AnimalShelter.Objects
{
  public class Type
  {
    private int _id;
    private string _animalType;
    public Type (string AnimalType, int Id = 0)
    {
      _id = Id;
      _animalType= AnimalType;
    }

    public override bool Equals(System.Object otherType)
    {
      if (!(otherType is Type))
      {
        return false;
      }
      else
      {
        Type newType = (Type) otherType;
        bool idEquality = this.GetId() == newType.GetId();
        bool typeEquality = this.GetType() == newType.GetType();
        return (idEquality && typeEquality);
      }
    }
    public int GetId()
    {
      return _id;
    }
    public string GetType()
    {
      return _animalType;
    }
    public void SetType(string newType)
    {
      _animalType = newType;
    }
    public static List<Type> GetAll()
    {
      List<Type> allTypes = new List<Type>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM types;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int typeId = rdr.GetInt32(0);
        string typeName = rdr.GetString(1);
        Type newType = new Type(typeName, typeId);
        allTypes.Add(newType);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allTypes;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO types (animal_type) OUTPUT INSERTED.id VALUES (@AnimalType);", conn);

      SqlParameter typeParameter = new SqlParameter();
      typeParameter.ParameterName = "@AnimalType";
      typeParameter.Value = this.GetType();
      cmd.Parameters.Add(typeParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }
    public static Type Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM types WHERE id = @TypeId;", conn);
      SqlParameter typeIdParameter = new SqlParameter();
      typeIdParameter.ParameterName = "@TypeId";
      typeIdParameter.Value = id.ToString();
      cmd.Parameters.Add(typeIdParameter);
      rdr = cmd.ExecuteReader();

      int foundTypeId = 0;
      string foundAnimalType = null;

      while(rdr.Read())
      {
        foundTypeId = rdr.GetInt32(0);
        foundAnimalType = rdr.GetString(1);
      }
      Type foundType = new Type(foundAnimalType, foundTypeId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundType;
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM types;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
