using DisneyAPI.Models;

public interface IUserServices
{
    User GetExistentUser(string email);
    List<User> GetAll();
    void CreateAsync(User user);

    User Get(User user);
    // Task SendMailAsync(string email);

}