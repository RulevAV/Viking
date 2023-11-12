import {observer} from "mobx-react-lite";
import React, {useEffect, useState} from "react";
import {Button, Input, message, Popconfirm} from "antd";
import set from "../../state/set";
import {DeleteFilled} from "@ant-design/icons";

interface setPropsType {
    id: string,
    setWeight: number,
    repetitionNuber: number,

}

const Set: React.FC<setPropsType>  = observer(({id,setWeight,repetitionNuber})=> {
    const [repNum,setRepNum] = useState(repetitionNuber)
    const [setWei,setSetWei] = useState(setWeight)

    const onChangeRepNum=(str:string)=>{
        if(!isNaN(Number(str))){
            setRepNum(Number(str))
        }
    }

    const onChangeSetWeight = (str:string) =>{
        if(!isNaN(Number(str))){
            setSetWei(Number(str))
        }
    }

    useEffect(()=>{
        // console.log(id)
    },[id])

    const confirm = (e: React.MouseEvent<HTMLElement> | undefined) => {
        set.deleteSet(id).then(t => {
            message.success('Успешно удалено');
        }).catch(e => {
            message.error('Ошибка')
        })
    };

    return (
        <div style={{display:'flex'}}>
            <Input
                title='Кол-во повторов'
                value={repNum}
                onChange={(e) => onChangeRepNum(e.target.value)}
                onBlur={(e) => set.updateSet(id,repNum,setWei)}
            >
            </Input>
            <Input
                title='Вес'
                value={setWei}
                onChange={(e) => onChangeSetWeight(e.target.value)}
                onBlur={(e) => set.updateSet(id,repNum,setWei)}
            >
            </Input>
            <Popconfirm
                title="Удалить подход"
                description="Уверены?"
                onConfirm={confirm}
                okText="Да"
                cancelText="Нет"
            >
                <Button size={"large"} icon={<DeleteFilled/>} className='bg-danger text-black-50'></Button>
            </Popconfirm>
        </div>
    )
});

export default Set;
