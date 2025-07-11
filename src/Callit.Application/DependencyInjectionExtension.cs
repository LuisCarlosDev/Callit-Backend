using Callit.Application.AutoMapper;
using Callit.Application.UseCases.SingIn.SignInUser;
using Callit.Application.UseCases.Tickets.CreateTicket;
using Callit.Application.UseCases.Tickets.DeleteTicket;
using Callit.Application.UseCases.Tickets.ListTicketById;
using Callit.Application.UseCases.Tickets.ListTickets;
using Callit.Application.UseCases.Tickets.UpdateTicket;
using Callit.Application.UseCases.Users.CreateUser;
using Callit.Application.UseCases.Users.ListUsers;
using Microsoft.Extensions.DependencyInjection;

namespace Callit.Application;

public static class DependencyInjectionExtension
{
  public static void AddApplication(this IServiceCollection services)
  {
    AddAutoMapper(services);
    AddUseCases(services);
  }

  private static void AddAutoMapper(IServiceCollection services)
  {
    services.AddAutoMapper(typeof(AutoMapping));
  }

  private static void AddUseCases(this IServiceCollection services)
  {
    services.AddScoped<ICreateTicketUseCase, CreateTicketUseCase>();
    services.AddScoped<IListTicketsUseCase, ListTicketsUseCase>();
    services.AddScoped<IListTicketByIdUseCase, ListTicketByIdUseCase>();
    services.AddScoped<IDeleteTicketUseCase, DeleteTicketUseCase>();
    services.AddScoped<IUpdateTicketUseCase, UpdateTicketUseCase>();
    services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
    services.AddScoped<ISignInUserUseCase, SignInUserUseCase>();
    services.AddScoped<IListUsersUseCase, ListUsersUseCase>();
  }
}
