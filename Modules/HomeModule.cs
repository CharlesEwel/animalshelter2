using System.Collections.Generic;
using Nancy;
using AnimalShelter.Objects;
using Nancy.ViewEngines.Razor;

namespace AnimalShelter
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["/animals/{orderingParameter}"] = parameters => {
        List<Animal> AllAnimals = Animal.GetAll(parameters.orderingParameter);
        return View["animals.cshtml", AllAnimals];
      };
      Get["/types"] = _ => {
        List<Type> AllTypes = Type.GetAll();
        return View["types.cshtml", AllTypes];
      };
      Get["/types/new"] = _ => {
        return View["types_form.cshtml"];
      };
      Post["/types/new"] = _ => {
        Type newType = new Type(Request.Form["animal_type"]);
        newType.Save();
        return View["success.cshtml"];
      };
      Get["/animals/new"] = _ => {
        List<Type> AllTypes = Type.GetAll();
        return View["animals_form.cshtml", AllTypes];
      };
      Post["/animals/new"] = _ => {
        Animal newAnimal = new Animal(Request.Form["animal-name"], Request.Form["animal-gender"], Request.Form["animal-breed"],  Request.Form["animal-admittance-date"], Request.Form["type-id"]);
        newAnimal.Save();
        return View["success.cshtml"];
      };
      Post["/animals/delete"] = _ => {
        Animal.DeleteAll();
        return View["cleared.cshtml"];
      };
      Get["/types/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var SelectedType = Type.Find(parameters.id);
        var TypeAnimals = Animal.GetAll("id");
        model.Add("types", SelectedType);
        model.Add("animals", TypeAnimals);
        return View["type.cshtml", model];
      };
      Get["/type/edit/{id}"] = parameters => {
        Type SelectedType = Type.Find(parameters.id);
        System.Console.WriteLine(SelectedType.GetType());
        return View["edit.cshtml", SelectedType];
      };
        Patch["type/edit/{id}"] = parameters => {
        Type SelectedType = Type.Find(parameters.id);
        SelectedType.Update(Request.Form["type-name"]);
        return View["success.cshtml"];
      };
    }
  }
}
