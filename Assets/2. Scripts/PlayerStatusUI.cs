using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatusUI : MonoBehaviour
{
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI latchCountText;

    // 예시 상태 변수
    public bool isStunned = false;
    public int latchCount = 0;

    void Update()
    {
        // 상태 업데이트
        statusText.text = isStunned ? "상태: 기절!" : "상태: 정상";

        // 매달린 횟수 표시
        latchCountText.text = $"매달린 횟수: {latchCount}";
    }
}