namespace MyProject.Entities.Models.Common
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
