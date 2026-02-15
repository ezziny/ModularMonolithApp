using System;
using MediatR;

namespace SharedKernel.CQRSStuff;

public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
{

}
public interface IQuery : IQuery<Unit> //basically making it void bc we can't do IQuery<Void>
{

}