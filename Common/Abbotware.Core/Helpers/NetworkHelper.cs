// -----------------------------------------------------------------------
// <copyright file="NetworkHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Helpers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;

    /// <summary>
    ///     Network helper utility functions
    /// </summary>
    public static class NetworkHelper
    {
        /// <summary>
        ///     Gets local MAC address
        /// </summary>
        /// <returns>mac address of first nic</returns>
        public static string FirstMacAddress()
        {
            var nic = NetworkInterface
                .GetAllNetworkInterfaces()
                .FirstOrDefault(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType != NetworkInterfaceType.Loopback);

            return nic?.GetPhysicalAddress().ToString() ?? string.Empty;
        }

        /// <summary>
        ///     returns all non loopback IPV4 addresses (if any)
        /// </summary>
        /// <returns>the list of IP addresses</returns>
        public static IPAddress[] LocalNonLoopbackIPV4()
        {
            var name = Environment.MachineName;

            try
            {
                var ips = Dns.GetHostAddresses(name);

                var ipArray = ips.Where(x => x.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(x))
                    .ToArray();

                if (ipArray.Any())
                {
                    return ipArray;
                }
            }
            catch (Exception)
            {
            }

            return LocalIpAdressViaUdpSocket();
        }

        /// <summary>
        ///     returns all non loopback IPV4 addresses (if any)
        /// </summary>
        /// <param name="address">IP address to check</param>
        /// <returns>true if the IP is a a non loopback IPV4 address</returns>
        public static bool IsLocalNonLoopbackIPV4(IPAddress address)
        {
            Arguments.NotNull(address, nameof(address));

            var localips = LocalNonLoopbackIPV4();

            return localips.Contains(address);
        }

        /// <summary>
        ///     Converts a subnet ip to CIDR
        /// </summary>
        /// <param name="subnetAddress">subnet mask</param>
        /// <returns>CIDR</returns>
        public static uint SubnetToCIDR(IPAddress subnetAddress)
        {
            subnetAddress = Arguments.EnsureNotNull(subnetAddress, nameof(subnetAddress));

            var ipParts = subnetAddress.GetAddressBytes();

            var subnet = 16777216 * Convert.ToUInt32(ipParts[0]);
            subnet += 65536 * Convert.ToUInt32(ipParts[1]);
            subnet += 256 * Convert.ToUInt32(ipParts[2]);
            subnet += Convert.ToUInt32(ipParts[3]);

            var mask = 0x80000000;

            var subnetConsecutiveOnes = 0;

            for (var i = 0; i < 32; i++)
            {
                if (!(mask & subnet).Equals(mask))
                {
                    break;
                }

                subnetConsecutiveOnes++;
                mask >>= 1;
            }

            checked
            {
                return (uint)subnetConsecutiveOnes;
            }
        }

        /// <summary>
        ///     uses a udp socket 'trick' to get the local ip address
        /// </summary>
        /// <remarks>https://stackoverflow.com/questions/6803073/get-local-ip-address</remarks>
        /// <returns>the list of IP addresses</returns>
        private static IPAddress[] LocalIpAdressViaUdpSocket()
        {
            using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0);

            socket.Connect("8.8.8.8", 65530);

            if (socket.LocalEndPoint is not IPEndPoint endPoint)
            {
                return Array.Empty<IPAddress>();
            }

            return new[] { endPoint.Address };
        }
    }
}