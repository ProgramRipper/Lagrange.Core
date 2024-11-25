using System.Text.Json;
using System.Text.Json.Nodes;
using Lagrange.Core;
using Lagrange.Core.Common.Interface.Api;
using Lagrange.Core.Message.Entity;
using Lagrange.OneBot.Core.Entity.Action;
using Lagrange.OneBot.Core.Operation.Converters;
using Lagrange.OneBot.Message.Entity;

namespace Lagrange.OneBot.Core.Operation.Group;

[Operation("get_group_at_all_remain")]
public class GetGroupAtAllRemainOperation : IOperation
{
    public async Task<OneBotResult> HandleOperation(BotContext context, JsonNode? payload)
    {
        var message = payload.Deserialize<OneBotGetGroupAtAllRemain>(SerializerOptions.DefaultOptions);
        if (message != null)
        {
            var (remainAtAllCountForUin, remainAtAllCountForGroup) = await context.GroupRemainAtAll(message.GroupId);
            return new OneBotResult(
                new JsonObject {
                    { "can_at_all", remainAtAllCountForGroup > 0 && remainAtAllCountForUin > 0 },
                    { "remain_at_all_count_for_group", remainAtAllCountForGroup },
                    { "remain_at_all_count_for_uin", remainAtAllCountForUin }
                },
                0,
                "ok"
            );
        }

        throw new Exception();
    }
}
