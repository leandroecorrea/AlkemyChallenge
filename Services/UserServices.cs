using DisneyAPI.Models;

namespace DisneyAPI.Services;

public class UserServices : IUserServices
{
    private DisneyContext _ctx;    
    private IMailingServices _mailingService;    

    public UserServices(DisneyContext ctx, IMailingServices mailingService)
    {
        _ctx = ctx;
        _mailingService = mailingService;
    }
    public async void CreateAsync(User user)
    {
        _ctx.Users.Add(user);
        _ctx.SaveChanges();
        await _mailingService.SendMailAsync(user.Email);
    }
    public User GetExistentUser(string email)
    {
        User user = _ctx.Users.SingleOrDefault(u=> u.Email == email);
        return user;
    }

    public User Get(User user)
    {
        User validUser = _ctx.Users.FirstOrDefault(u => (u.Email == user.Email && u.Password == user.Password));
        return validUser;
    }
    public List<User> GetAll()
    {
        return _ctx.Users.ToList();
    }
}