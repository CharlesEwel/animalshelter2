using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AnimalShelter.Objects
{
  public class TypeTest : IDisposable
  {
    public TypeTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=AnimalShelter_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_TypesEmptyAtFirst()
    {
      int result = Type.GetAll().Count;
      Assert.Equal(0, result);
    }
    public void Dispose()
    {
      Type.DeleteAll();
    }
    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      //Arrange, Act
      Type firstType = new Type("Dog");
      Type secondType = new Type("Dog");

      //Assert
      Assert.Equal(firstType, secondType);
    }
    [Fact]
    public void Test_Save_SavesTypeToDatabase()
    {
      //Arrange
      Type testType = new Type("Dog");
      testType.Save();

      //Act
      List<Type> result = Type.GetAll();
      List<Type> testList = new List<Type>{testType};

      //Assert
      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test_Save_AssignsIdToTypeObject()
    {
      //Arrange
      Type testType = new Type("Dog");
      testType.Save();

      //Act
      Type savedType = Type.GetAll()[0];

      int result = savedType.GetId();
      int testId = testType.GetId();

      //Assert
      Assert.Equal(testId, result);
    }
    [Fact]
    public void Test_Find_FindsTypeInDatabase()
    {
      //Arrange
      Type testType = new Type("Household chores");
      testType.Save();

      //Act
      Type foundType = Type.Find(testType.GetId());

      //Assert
      Assert.Equal(testType, foundType);
    }
  }
}
