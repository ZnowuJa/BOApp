using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Graph.Models;

namespace Application.Interfaces;
public interface IEmailService
{
    Task SendEmailAsync(Message message);
}
