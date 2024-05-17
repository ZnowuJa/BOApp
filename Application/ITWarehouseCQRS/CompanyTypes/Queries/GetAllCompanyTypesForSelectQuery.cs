﻿using Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.CompanyTypes.Queries;
public class GetAllCompanyTypesForSelectQuery : IRequest<IQueryable<CompanyTypeVm>>
{
}

