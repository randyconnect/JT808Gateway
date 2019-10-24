﻿using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System.Threading;
using JT808.Gateway.PubSub;

namespace JT808.Gateway.BusinessServices.Transmit
{
    public class JT808DotNettyTransmitHostedService:IHostedService
    {
        private readonly JT808DotNettyTransmitService jT808DotNettyTransmitService;
        private readonly IJT808MsgConsumer jT808MsgConsumer;
        public JT808DotNettyTransmitHostedService(
            IJT808MsgConsumer jT808MsgConsumer,
            JT808DotNettyTransmitService jT808DotNettyTransmitService)
        {
            this.jT808DotNettyTransmitService = jT808DotNettyTransmitService;
            this.jT808MsgConsumer = jT808MsgConsumer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            jT808MsgConsumer.Subscribe();
            jT808MsgConsumer.OnMessage(jT808DotNettyTransmitService.Send);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            jT808MsgConsumer.Unsubscribe();
            return Task.CompletedTask;
        }
    }
}
