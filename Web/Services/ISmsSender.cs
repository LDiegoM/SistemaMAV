﻿namespace SistemaMAV.Web.Services;

public interface ISmsSender
{
    Task SendSmsAsync(string number, string message);
}
