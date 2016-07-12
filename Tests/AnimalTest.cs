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
     int result = Animal.GetAll().Count;
     Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueIfNamesAndTypesAreTheSame()
    {
     //Arrange, Act
    //  DateTime sampleDate = new DateTime(1990,09,05);
     Animal firstAnimal = new Animal("Bob", 2);
     Animal secondAnimal = new Animal("Bob", 2);

     //Assert
     Assert.Equal(firstAnimal, secondAnimal);
    }
    public void Dispose()
    {
      Animal.DeleteAll();
    }
  }
}
