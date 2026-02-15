using System;
using MediatR;
using SharedKernel.CQRSStuff;

namespace SharedKernel.CQRSStuff;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
where TQuery : IQuery<TResponse> where TResponse : notnull
{

}
