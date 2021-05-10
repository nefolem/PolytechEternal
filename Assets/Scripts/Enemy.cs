using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // int hSpeed;
    // int vSpeed;

    // int enemyDamage;
     float attackRadius;

    // int maxHealth;
    // int currentHealth;

     float followRadius;

    // //public Animator anim;

    // public void setHorizontalSpeed(int speed)
    // {
    //     hSpeed = speed;
    // }

    // public void setVerticalSpeed(int speed)
    // {
    //     vSpeed = speed;
    // }

    // public void setAttackDamage(int attdmg)
    // {
    //     enemyDamage = attdmg;
    // }

    // public void setMaxHealthPoints(int mhp)
    // {
    //     maxHealth = mhp;
    // }

    // public void setCurrentHealthPoints(int chp)
    // {
    //     currentHealth = chp;
    // }

    // public int getHorizontalSpeed()
    // {
    //     return hSpeed;
    // }

    // public int getVerticalSpeed()
    // {
    //     return vSpeed;
    // }

    // public int getAttackDamage()
    // {
    //     return enemyDamage;
    // }

    // public int getMaxHealthPoints()
    // {
    //     return maxHealth;
    // }

    // public int getCurrentHealthPoints()
    // {
    //     return currentHealth;
    // }


    //movement toward a player
    public void setFollowRadius(float r)
    {
        followRadius = r;
    }
    //attack radius 
    public void setAttackRadius(float r)
    {
        attackRadius = r;
    }

    //if player in radius move toward him 
    public bool checkFollowRadius(float playerPosition, float enemyPosition)
    {
        if(Mathf.Abs(playerPosition - enemyPosition) < followRadius)
        {
            //player in range
            return true;
        }
        else
        {
            return false;
        }
    }

    //if player in radius attack him
    public bool checkAttackRadius(float playerPosition, float enemyPosition)
    {
        if (Mathf.Abs(playerPosition - enemyPosition) < attackRadius)
        {
            //in range for attack
            return true;
        }
        else
        {
            return false;
        }
    }

    

 
}
