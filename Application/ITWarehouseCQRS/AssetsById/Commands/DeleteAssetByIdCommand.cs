﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.AssetsById.Commands;
public class DeleteAssetByIdCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteAssetByIdCommand(int id)
    {
        Id = id;
    }
}
