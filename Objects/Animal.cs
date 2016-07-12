using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace AnimalShelter.Objects
{
  public class Animal
  {
    private int _id;
    private string _name;
    private int _typeId;

    public Animal(string name, int typeId, int Id = 0)
    {
      _id = Id;
      _name = name;
      _typeId = typeId;
    }
    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public void SetName(string newName)
    {
      _name = newName;
    }
    public int GetTypeId()
    {
      return _typeId;
    }
    public void SetTypeId(int newTypeId)
    {
      _typeId = newTypeId;
    }

    public override bool Equals(System.Object otherAnimal)
    {
      if(!(otherAnimal is Animal))
      {
        return false;
      }
      else
      {
        Animal newAnimal = (Animal) otherAnimal;
        bool idEquality = (this.GetId() == newAnimal.GetId());
        bool nameEquality = (this.GetName()== newAnimal.GetName());
        bool typeEquality = this.GetTypeId() == newAnimal.GetTypeId();
        return (idEquality && nameEquality && typeEquality);
      }
    }
    public static List<Animal> GetAll()
    {
      List<Animal> allAnimals = new List<Animal>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM animals;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int animalId = rdr.GetInt32(0);
        string animalName = rdr.GetString(1);
        int animalTypeId = rdr.GetInt32(2);
        Animal newAnimal = new Animal(animalName, animalTypeId, animalId);
        allAnimals.Add(newAnimal);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allAnimals;
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM animals;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
