using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    public int value;
    public int displayValue;
    public float spacing;
    public Sprite[] spriteDigits;

    // Update is called once per frame
    void Update()
    {
        //이 값이 변하면 스프라이트를 다시 그려 주어야 한다.
        if(displayValue != value) {
            //숫자 값을 문자열로 반환
            string digits = value.ToString();
            //자식에 등록되어있는 SpriteRenderer 컴포넌트를 얻는다
            SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();

            //렌더러 갯수를 얻음
            int numRenderers = renderers.Length;

            //SpriteRenderer를 충분히 가지고 있지 않으면 (하나마다 스프라이트 렌더러를 가진다.) 추가한다.
            if(numRenderers < digits.Length) {
                //개수가 충분할 때까지 등록한다
                while(numRenderers < digits.Length) {
                    GameObject spr = new GameObject();

                    spr.AddComponent<SpriteRenderer>();

                    spr.transform.parent = transform;

                    spr.transform.localPosition = new Vector3(
                        numRenderers * spacing, 0.0f, 0.0f
                    );

                    spr.layer = 5;
                    numRenderers = numRenderers + 1;
                }

                renderers = GetComponentsInChildren<SpriteRenderer>();
            }

            //렌더러를 필요 이상으로 많이 가지고 있다면 숨긴다
            else if(numRenderers > digits.Length) {
                while(numRenderers > digits.Length) {

                    //스프라이트 삭제
                    renderers[numRenderers - 1].sprite = null;

                    //렌더러 개수를 감소시켜 루프를 무한히 실행하는 것을 방지
                    //note : this isn't actually deleting the renderer
                    numRenderers = numRenderers - 1;
                }
            }
            
            //값에 따라 스프라이트를 설정한다.
            int rendererIndex = 0;
            foreach(char digit in digits) {
                //숫자 값을 인덱스로 변환한다.
                int spriteIndex = int.Parse(digit.ToString());
                //가장 왼쪽의 렌더러를 시작으로 스프라이트를 설정
                //spriteDigits 배열의 0번째 요소가 숫자 0과 같다.
                renderers[rendererIndex].sprite = spriteDigits[spriteIndex];
                //렌더러 인덱스를 증가시켜 다음에 접근할 렌더러를 결정한다.
                rendererIndex = rendererIndex + 1;
            }
            displayValue = value;
        }
    }
}
