﻿using Middlink.Core.CQRS.Commands;
using System;

namespace Employee.Contract.Commands
{
    public record CreateEmployee(Guid AggregateId, string RegistrationNumber, string Surname, int Gender) : ICommand<Guid>;

}
