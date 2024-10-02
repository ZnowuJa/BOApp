using System.Text.Json;

using Application.Forms;
using Application.Interfaces;
using Application.ViewModels.CoC;
using Application.ViewModels.General;

using AutoMapper;

using Domain.Forms;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models;

namespace Application.CQRS.CoCCQRS.Onboarding.Commands;

