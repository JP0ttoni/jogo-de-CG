using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class passowrd_button : MonoBehaviour
{
    private int first, second, third, i = 0;
    public TextMeshProUGUI txt;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(i >= 3)
        {
            if( first == 5 && second == 6 && third == 7)
            {
                txt.text = "acertou";
                StartCoroutine(acerto());
            } else{
                i = 0;
                first = 10;
                second = 10;
                third = 10;
                txt.text = "errou";
                life_keeper.keep_life--;
                StartCoroutine(cd());
            }
        }
    }

    public void b1()
    {
        if(i == 0)
        {
            first = 1;
            i++;
        } else if(i == 1)
        {
            second = 1;
            i++;
        } else if(i == 2)
        {
            third = 1;
            i++;
        }
    }

    public void b2()
    {
        if(i == 0)
        {
            first = 2;
            i++;
        } else if(i == 1)
        {
            second = 2;
            i++;
        } else if(i == 2)
        {
            third = 2;
            i++;
        }
    }

    public void b3()
    {
        if(i == 0)
        {
            first = 3;
            i++;
        } else if(i == 1)
        {
            second = 3;
            i++;
        } else if(i == 2)
        {
            third = 3;
            i++;
        }
    }

    public void b4()
    {
        if(i == 0)
        {
            first = 4;
            i++;
        } else if(i == 1)
        {
            second = 4;
            i++;
        } else if(i == 2)
        {
            third = 4;
            i++;
        }
    }

    public void b5()
    {
        if(i == 0)
        {
            first = 5;
            i++;
        } else if(i == 1)
        {
            second = 5;
            i++;
        } else if(i == 2)
        {
            third = 5;
            i++;
        }
    }

    public void b6()
    {
        if(i == 0)
        {
            first = 6;
            i++;
        } else if(i == 1)
        {
            second = 6;
            i++;
        } else if(i == 2)
        {
            third = 6;
            i++;
        }
    }

    public void b7()
    {
        if(i == 0)
        {
            first = 7;
            i++;
        } else if(i == 1)
        {
            second = 7;
            i++;
        } else if(i == 2)
        {
            third = 7;
            i++;
        }
    }

    public void b8()
    {
        if(i == 0)
        {
            first = 8;
            i++;
        } else if(i == 1)
        {
            second = 8;
            i++;
        } else if(i == 2)
        {
            third = 8;
            i++;
        }
    }

    public void b9()
    {
        if(i == 0)
        {
            first = 9;
            i++;
        } else if(i == 1)
        {
            second = 9;
            i++;
        } else if(i == 2)
        {
            third = 9;
            i++;
        }
    }

    public void b0()
    {
        if(i == 0)
        {
            first = 0;
            i++;
        } else if(i == 1)
        {
            second = 0;
            i++;
        } else if(i == 2)
        {
            third = 0;
            i++;
        }
    }

    private IEnumerator cd()
    {
        yield return new WaitForSeconds(1);
        txt.text = "digite:";
    }

    private IEnumerator acerto()
    {
        yield return new WaitForSeconds(1);
        ControladorDasChaves.chavesDoJogador.Add(3);
        anim.SetTrigger("getup");
        CharacterControll.podeMover = true;
        Destroy(gameObject);
    }
}
