using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace AnimalShelter.Objects
{
  public class Animal
  {
    private int _id;
    private string _name;
    private string _gender;
    private string _breed;
    private DateTime _admittanceDate;
    private int _typeId;

    public Animal(string name, string gender, string breed, DateTime admittanceDate, int typeId, int Id = 0)
    {
      _id = Id;
      _name = name;
      _gender = gender;
      _breed = breed;
      _admittanceDate = admittanceDate;
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
    public string GetGender()
    {
      return _gender;
    }
    public string GetBreed()
    {
      return _breed;
    }
    public DateTime GetAdmittanceDate()
    {
      return _admittanceDate;
    }
    public int GetTypeId()
    {
      return _typeId;
    }

    public void SetName(string newName)
    {
      _name = newName;
    }
    public void SetGender(string newGender)
    {
      _gender = newGender;
    }
    public void SetBreed(string newBreed)
    {
      _breed = newBreed;
    }
    public void SetAdmittanceDate(DateTime newAdmittanceDate)
    {
      _admittanceDate = newAdmittanceDate;
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
        bool genderEquality = (this.GetGender()== newAnimal.GetGender());
        bool breedEquality = (this.GetBreed()== newAnimal.GetBreed());
        bool admittanceDateEquality = (this.GetAdmittanceDate()== newAnimal.GetAdmittanceDate());
        bool typeEquality = this.GetTypeId() == newAnimal.GetTypeId();
        return (idEquality && nameEquality && genderEquality && breedEquality && typeEquality);
      }
    }
    public static List<Animal> GetAll(string orderingParameter)
    {
      List<Animal> allAnimals = new List<Animal>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM animals ORDER BY "+orderingParameter+";", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int animalId = rdr.GetInt32(0);
        string animalName = rdr.GetString(1);
        string animalGender = rdr.GetString(2);
        string animalBreed = rdr.GetString(3);
        DateTime admittanceDate = rdr.GetDateTime(4);
        int animalTypeId = rdr.GetInt32(5);
        Animal newAnimal = new Animal(animalName, animalGender, animalBreed, admittanceDate, animalTypeId, animalId);
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

    public void Save()
  {
    SqlConnection conn = DB.Connection();
    SqlDataReader rdr;
    conn.Open();

     SqlCommand cmd = new SqlCommand("INSERT INTO animals (name, gender, breed, admittance_date, type_id) OUTPUT INSERTED.id VALUES (@AnimalName, @AnimalGender, @AnimalBreed, @AdmittanceDate, @TypeId);", conn);

    SqlParameter nameParameter = new SqlParameter();
    nameParameter.ParameterName = "@AnimalName";
    nameParameter.Value = this.GetName();

    SqlParameter genderParameter = new SqlParameter();
    genderParameter.ParameterName = "@AnimalGender";
    genderParameter.Value = this.GetGender();

    SqlParameter breedParameter = new SqlParameter();
    breedParameter.ParameterName = "@AnimalBreed";
    breedParameter.Value = this.GetBreed();

    SqlParameter admittanceDateParameter = new SqlParameter();
    admittanceDateParameter.ParameterName = "@AdmittanceDate";
    admittanceDateParameter.Value = this.GetAdmittanceDate();

    SqlParameter typeIdParameter = new SqlParameter();
    typeIdParameter.ParameterName = "@TypeId";
    typeIdParameter.Value = this.GetTypeId();

    cmd.Parameters.Add(nameParameter);
    cmd.Parameters.Add(genderParameter);
    cmd.Parameters.Add(breedParameter);
    cmd.Parameters.Add(admittanceDateParameter);
    cmd.Parameters.Add(typeIdParameter);

    rdr = cmd.ExecuteReader();

    while(rdr.Read())
    {
      this._id = rdr.GetInt32(0);
    }
    if (rdr != null)
    {
      rdr.Close();
    }
    if (conn != null)
    {
      conn.Close();
    }
  }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM animals;", conn);
      cmd.ExecuteNonQuery();
    }
      public static Animal Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM animals WHERE id=@AnimalId;", conn);
      SqlParameter animalIdParameter = new SqlParameter();
      animalIdParameter.ParameterName = "@AnimalId";
      animalIdParameter.Value = id.ToString();
      cmd.Parameters.Add(animalIdParameter);
      rdr = cmd.ExecuteReader();

      int foundAnimalId = 0;
      string foundAnimalName = null;
      string foundAnimalGender = null;
      string foundAnimalBreed = null;
      DateTime foundAdmittanceDate = new DateTime(1900, 09, 05);
      int foundAnimalTypeId = 0;

      while(rdr.Read())
      {
        foundAnimalId = rdr.GetInt32(0);
        foundAnimalName = rdr.GetString(1);
        foundAnimalGender = rdr.GetString(2);
        foundAnimalBreed = rdr.GetString(3);
        foundAdmittanceDate = rdr.GetDateTime(4);
        foundAnimalTypeId = rdr.GetInt32(5);
      }
      Animal foundAnimal = new Animal(foundAnimalName, foundAnimalGender, foundAnimalBreed, foundAdmittanceDate, foundAnimalTypeId, foundAnimalId);
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundAnimal;
    }
  }
}
