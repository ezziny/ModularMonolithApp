using System;
using MediatR;

namespace SharedKernel.CQRSStuff;

public interface ICommand<out TResponse> : IRequest<TResponse>
{

}

public interface ICommand: ICommand<Unit> //basically making it void bc we can't do ICommand<Void>
{
    
}