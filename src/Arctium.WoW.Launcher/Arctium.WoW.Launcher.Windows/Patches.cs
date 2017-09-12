// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Arctium.WoW.Launcher.Windows
{
    public class Patches
    {
        public static class Common
        {
            public static byte[] Modulus = { 0x5F, 0xD6, 0x80, 0x0B, 0xA7, 0xFF, 0x01, 0x40, 0xC7, 0xBC, 0x8E, 0xF5, 0x6B, 0x27, 0xB0, 0xBF,
                                             0xF0, 0x1D, 0x1B, 0xFE, 0xDD, 0x0B, 0x1F, 0x3D, 0xB6, 0x6F, 0x1A, 0x48, 0x0D, 0xFB, 0x51, 0x08,
                                             0x65, 0x58, 0x4F, 0xDB, 0x5C, 0x6E, 0xCF, 0x64, 0xCB, 0xC1, 0x6B, 0x2E, 0xB8, 0x0F, 0x5D, 0x08,
                                             0x5D, 0x89, 0x06, 0xA9, 0x77, 0x8B, 0x9E, 0xAA, 0x04, 0xB0, 0x83, 0x10, 0xE2, 0x15, 0x4D, 0x08,
                                             0x77, 0xD4, 0x7A, 0x0E, 0x5A, 0xB0, 0xBB, 0x00, 0x61, 0xD7, 0xA6, 0x75, 0xDF, 0x06, 0x64, 0x88,
                                             0xBB, 0xB9, 0xCA, 0xB0, 0x18, 0x8B, 0x54, 0x13, 0xE2, 0xCB, 0x33, 0xDF, 0x17, 0xD8, 0xDA, 0xA9,
                                             0xA5, 0x60, 0xA3, 0x1F, 0x4E, 0x27, 0x05, 0x98, 0x6F, 0xAA, 0xEE, 0x14, 0x3B, 0xF3, 0x97, 0xA8,
                                             0x12, 0x02, 0x94, 0x0D, 0x84, 0xDC, 0x0E, 0xF1, 0x76, 0x23, 0x95, 0x36, 0x13, 0xF9, 0xA9, 0xC5,
                                             0x48, 0xDB, 0xDA, 0x86, 0xBE, 0x29, 0x22, 0x54, 0x44, 0x9D, 0x9F, 0x80, 0x7B, 0x07, 0x80, 0x30,
                                             0xEA, 0xD2, 0x83, 0xCC, 0xCE, 0x37, 0xD1, 0xD1, 0xCF, 0x85, 0xBE, 0x91, 0x25, 0xCE, 0xC0, 0xCC,
                                             0x55, 0xC8, 0xC0, 0xFB, 0x38, 0xC5, 0x49, 0x03, 0x6A, 0x02, 0xA9, 0x9F, 0x9F, 0x86, 0xFB, 0xC7,
                                             0xCB, 0xC6, 0xA5, 0x82, 0xA2, 0x30, 0xC2, 0xAC, 0xE6, 0x98, 0xDA, 0x83, 0x64, 0x43, 0x7F, 0x0D,
                                             0x13, 0x18, 0xEB, 0x90, 0x53, 0x5B, 0x37, 0x6B, 0xE6, 0x0D, 0x80, 0x1E, 0xEF, 0xED, 0xC7, 0xB8,
                                             0x68, 0x9B, 0x4C, 0x09, 0x7B, 0x60, 0xB2, 0x57, 0xD8, 0x59, 0x8D, 0x7F, 0xEA, 0xCD, 0xEB, 0xC4,
                                             0x60, 0x9F, 0x45, 0x7A, 0xA9, 0x26, 0x8A, 0x2F, 0x85, 0x0C, 0xF2, 0x19, 0xC6, 0x53, 0x92, 0xF7,
                                             0xF0, 0xB8, 0x32, 0xCB, 0x5B, 0x66, 0xCE, 0x51, 0x54, 0xB4, 0xC3, 0xD3, 0xD4, 0xDC, 0xB3, 0xEE };

            // Our own ca_bundle.txt.signed file.
            // Supported certificates:
            // - Arctium Emulation
            public static string CertBundleData = "eyJDcmVhdGVkIjoxNDYzNDA3MjM4LCJDZXJ0aWZpY2F0ZXMiOlt7IlVyaSI6IiouKiIsIlNoYUhhc2hQdWJsaWNLZXlJbmZvIjoiOTJDNEZDQTJCRjgxRTA2OEQwODAwQjcwN0U4MENGNDNFRTczRTYzQjQ2RTdBNzczNjY1OTdBMTI1QkQ2Njg0QyJ9XSwiUHVibGljS2V5cyI6W3siVXJpIjoiKi4qIiwiU2hhSGFzaFB1YmxpY0tleUluZm8iOiI5MkM0RkNBMkJGODFFMDY4RDA4MDBCNzA3RTgwQ0Y0M0VFNzNFNjNCNDZFN0E3NzM2NjU5N0ExMjVCRDY2ODRDIn1dLCJTaWduaW5nQ2VydGlmaWNhdGVzIjpbeyJSYXdEYXRhIjoiLS0tLS1CRUdJTiBDRVJUSUZJQ0FURS0tLS0tTUlJRnpUQ0NBN1dnQXdJQkFnSUpBTk5yN2x0dU9ydkVNQTBHQ1NxR1NJYjNEUUVCQ3dVQU1Id3hDekFKQmdOVkJBWVRBa1JGTVJNd0VRWURWUVFJREFwVGIyMWxMVk4wWVhSbE1Sb3dHQVlEVlFRS0RCRkJjbU4wYVhWdElFVnRkV3hoZEdsdmJqRWRNQnNHQTFVRUN3d1VRWEpqZEdsMWJTQkZiWFZzWVhScGIyNGdRMEV4SFRBYkJnTlZCQU1NRkVGeVkzUnBkVzBnUlcxMWJHRjBhVzl1SUVOQk1DQVhEVEUyTURVeE5qRTVNRE16TkZvWUR6SXdOVFl3TlRBMk1Ua3dNek0wV2pCOE1Rc3dDUVlEVlFRR0V3SkVSVEVUTUJFR0ExVUVDQXdLVTI5dFpTMVRkR0YwWlRFYU1CZ0dBMVVFQ2d3UlFYSmpkR2wxYlNCRmJYVnNZWFJwYjI0eEhUQWJCZ05WQkFzTUZFRnlZM1JwZFcwZ1JXMTFiR0YwYVc5dUlFTkJNUjB3R3dZRFZRUUREQlJCY21OMGFYVnRJRVZ0ZFd4aGRHbHZiaUJEUVRDQ0FpSXdEUVlKS29aSWh2Y05BUUVCQlFBRGdnSVBBRENDQWdvQ2dnSUJBTDJ6R08vVGlCbTg5S0trWVl5YTJoMnpRaEtVMDZPZWpPRGtZQy9QQnRGOWREUlEvUTcrNGp6TitKdUErVGwzdFRqQ1UxNldIVlhQS1dJdE03VzV3Q3VGU3dmR1pSSHlxTC9DSTl0dTJRYWxVVVBRV0FMUm9QTkdQZENsV285R3J0ekhpbmRhKzZacElCZDFLZFhlRUVueHQwaXgrRUpGKzFZSm95bnV2UnIweUJvMW5SQTduNjhPMDBnTTB0aFprRW5QN3UrVStlZ1pacFNONTQ3TU1JUW8xaWpQOTVXanh3YUFQL1BHNzc0bGd3dXVDUTV2Tko1Q0dod09tUjMwUHBRSjdEZEViMU5QUUNYNEpaSFhrdGcyS2tSS3RPT2hiMWg3YW80M1VnMzBRWEpDQXFBeWZhbStmTEhVWmZDNXJLUmFibTFrcVdxVDBNa1dHMCsyekVZYi9iZWp6WVZlV1EydzJKVmh3VTNqYWY0Ty9wSWNBMzh4THF6VEhBMHUzWGsrN09yaUh1MUMxaXhrSmorVlhzN2E4UjZ5QzJkT2psYjEvRjQwOCs2UWUwWldMTFNKc0M2d1ZOajJoN1AyTm1QaDYzSm5WaTkzVWc5UWUxR1YzVG9vZ0pVbUM2ak9LZWJ1enhkekF6VnB6SEpJT3ZDNmR5ZWNIRHhUdnFhU1NGMHF1ejhPaCtkeEdiWHdCeWYrakpDdzdLRkVUUHZyL3JrUXZiMmVCSTRpdmNHSUNBYmMzNjdycm5RNFNXRWU2Syt6cE1LN3ZxakVIbUs2ZVJGMGU3ZExXTVR5elJ4d21XREJWQkwwRlowTmdBekwwMXd2VzFYMVVsQ29aUkplVzFhS1hhZVJkSXdSV01aU0VPeEdpSnZvZlFCQWNNa2xmVTdiazBZSW1vZDVBZ01CQUFHalVEQk9NQjBHQTFVZERnUVdCQlF0RExpdUhWMncxT3FJcUI3b3h4NGR2NUpLM1RBZkJnTlZIU01FR0RBV2dCUXRETGl1SFYydzFPcUlxQjdveHg0ZHY1SkszVEFNQmdOVkhSTUVCVEFEQVFIL01BMEdDU3FHU0liM0RRRUJDd1VBQTRJQ0FRQjVkcmZidDNkVlZYWWNacDUzRzRNMWIzc01rUVpwYUZnS3JheHRHcEZsd0JXQkh3Zy9IT3NJY3JRTFFtV0V2UU1uZFpsUC93L09oNWZtcmJJLzBlOXY5MlREeUcveE5Qb0h1clprWDFvbE9panVhLy9MbFlFcE9JWW1Hb2gyUXZaK2IyVkVWdDZUdnR3UmovTFJlUDhIUlpmY1NHTlh1VFkvazdNRnNNRkV4Smo4L0hNeCtyUVpFcE9NcXdpV2QzdFUxZmcrblpscW9xS2UzWWxUSHBNdW43dFBzRkMwVTA5NzZyY1Y0eWhsYjJTS3Z4eXJLd2MrRXliQXl3R2JzMEJiS0tkWWQ5N0ZrZnU2eTBCcXpiZVMzc2Z4L1BPejNVekE5YlRNK1VqSy9tVktmNTFKR0p0QVl2QUVRUnRDdjJxbUEwRGRBVG4zeUVjTWY3OENDKy9pZ2wvcnNGT3lqeUN1Z2MweUU2cVFIVkE5ZkpKZTdRSDJ1Uk9RWGVYTWQyU2NVV2tPMTRSUXE4aE5RclpCL3ZHVEgzZUVjSnFmWDZSVnpvengzbDlsQUxENllvZDNQTGlxc2dNUTRjbDdRVFRSUHpKc1FUcFZkUXhHK0hHV1dveHRRMjhkOTJYVU9GMjZtQkVDeXlwMCtubkpWZUdiaG10ZFl5RkdERWtwWEhSNjJ5aHI2eitFSlV0U2dIaFp5U3Z4aTVJcElFeFN0dnFlVGo0NVZNcmtnRXZubGtNTi9lbWFtSURhMEhVK2xXSzl1Ym5IZFJXSDV0VkJBSkc0N2Vyd0pjQ2pXWDNQc1ZQWENlQ25CcFRHcjJZaXZJdFNmV0lLRWxIbU9MbVBHYVpXN3Y4enlubUhIVG9QaC8yREFyT2sxZXhXZzVnclV4RWcydnRCQzJBVHhRPT0tLS0tLUVORCBDRVJUSUZJQ0FURS0tLS0tIn1dfQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
        }

        public static class Live
        {
            public static class Win64
            {
                // TODO: Remove Connect patch. Arctium only.
                public static byte[] Connect    = { 0xEB };
                public static byte[] CertBundle = { 0x41, 0xB1, 0x01 };
                public static byte[] Signature  = { 0xEB, 0x02, 0xB3, 0x01 };
            }
        }

        public static class Ptr
        {
            public static class Win64
            {
                // TODO: Remove Connect patch. Arctium only.
                public static byte[] Connect    = { 0xEB };
                public static byte[] CertBundle = { 0x90, 0x88 };
                public static byte[] Signature  = { 0xB3, 0x01 };
            }
        }
    }
}
