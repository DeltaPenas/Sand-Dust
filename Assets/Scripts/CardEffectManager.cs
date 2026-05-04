using UnityEngine;

public static class CardEffectManager
{
    public static void ApplyEffect(string cardId)
    {
        var player = Object.FindAnyObjectByType<PlayerController>();

        if (player == null)
        {
            Debug.LogWarning("PlayerController não encontrado.");
            return;
        }

        switch (cardId)
        {
            case "vida_up":
                player.currentStatus.vidaMax += 2;
                break;

            case "dano_up":
                player.currentStatus.danoRanged += 5;
                break;

            case "velocidade_up":
                player.currentStatus.velocidade += 1f;
                break;
        }
    }
}