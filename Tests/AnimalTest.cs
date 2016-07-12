using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AnimalShelter.Objects
{
  public class AnimalShelterTest : IDisposable
  {
    public AnimalShelterTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=AnimalShelter_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
     int result = Animal.GetAll("id").Count;
     Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueIfNamesAndTypesAreTheSame()
    {
     //Arrange, Act
     DateTime sampleDate = new DateTime(1990,09,05);
     Animal firstAnimal = new Animal("Bob", "Male", "German Sheperd", sampleDate, 2);
     Animal secondAnimal = new Animal("Bob", "Male", "German Sheperd", sampleDate, 2);

     //Assert
     Assert.Equal(firstAnimal, secondAnimal);
    }
    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      DateTime sampleDate = new DateTime(1990,09,05);
      Animal testAnimal = new Animal("Fluffy", "Female", "Persian", sampleDate, 1);

      //Act
      testAnimal.Save();
      List<Animal> result = Animal.GetAll("id");
      List<Animal> testList = new List<Animal>{testAnimal};

      //Assert
      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      //Arrange
      DateTime sampleDate = new DateTime(1990,09,05);
      Animal testAnimal = new Animal("Fluffy", "Female", "Persian", sampleDate, 1);

      //Act
      testAnimal.Save();
      Animal savedAnimal = Animal.GetAll("id")[0];

      int result = savedAnimal.GetId();
      int testId = testAnimal.GetId();

      //Assert
      Assert.Equal(testId, result);
    }
    [Fact]
    public void Test_Find_FindsAnimalInDatabase()
    {
      //Arrange
      DateTime sampleDate = new DateTime(1990,09,05);
      Animal testAnimal = new Animal("Fluffy", "Female", "Persian", sampleDate, 1);
      testAnimal.Save();

      //Act
      Animal foundAnimal = Animal.Find(testAnimal.GetId());

      //Assert
      Assert.Equal(testAnimal, foundAnimal);
    }
    public void Dispose()
    {
      Animal.DeleteAll();
    }
  }
}
