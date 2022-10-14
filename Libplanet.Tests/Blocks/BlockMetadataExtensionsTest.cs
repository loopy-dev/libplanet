using System;
using Libplanet.Blocks;
using Libplanet.Crypto;
using Libplanet.Tests.Fixtures;
using Xunit;
using static Libplanet.Tests.TestUtils;

namespace Libplanet.Tests.Blocks
{
    public class BlockMetadataExtensionsTest : BlockContentFixture
    {
        [Fact]
        public void ValidateTimestamp()
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            DateTimeOffset future = now + TimeSpan.FromSeconds(17);
            PublicKey publicKey = new PrivateKey().PublicKey;
            IBlockMetadata metadata = new BlockMetadata(
                protocolVersion: BlockMetadata.CurrentProtocolVersion,
                index: 0,
                timestamp: future,
                miner: publicKey.ToAddress(),
                publicKey: publicKey,
                previousHash: null,
                txHash: null,
                lastCommit: null);
            Assert.Throws<InvalidBlockTimestampException>(() => metadata.ValidateTimestamp(now));

            // It's okay because 3 seconds later.
            metadata.ValidateTimestamp(now + TimeSpan.FromSeconds(3));
        }
    }
}
