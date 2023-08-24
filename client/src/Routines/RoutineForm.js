import { useState, useEffect } from "react"
import { useNavigate } from "react-router-dom"
import { addRoutine } from "../Managers/RoutineManager"
import { getAllPoses } from "../Managers/PoseManager"
import { Button, Form, FormGroup, Input, Label } from "reactstrap"


export const RoutineForm = () => {
    const localSoulFlyUser = localStorage.getItem("users");
    const soulFlyUserObject = JSON.parse(localSoulFlyUser);
    const [poses, setPoses] = useState([]);
    const navigate = useNavigate();

    const getPoses = () => {
        getAllPoses().then(allPoses => setPoses(allPoses));
    }

        useEffect(() => {
            getPoses()
        }, [])

    const [newRoutine, updateRoutine] = useState({
        intention: "",
        cycles: 0,
        poseId: "",
        reflection: "",
        creationDate: Date.now(),
        userId: soulFlyUserObject.id
    })
   

    const handleSaveButtonClick = (event) => {
        event.preventDefault()
        console.log("So you like to push buttons?")

        if (newRoutine.poseId === "") {
            alert("Please select a pose");
            return;
        }

        const routineToSendToAPI = {
            Intention: newRoutine.intention,
            Reflection: newRoutine.reflection,
            CreationDate: new Date(),
            Cycles: newRoutine.cycles,
            PoseId: newRoutine.poseId,
            UserId: soulFlyUserObject.id
        }

        addRoutine(routineToSendToAPI).then((routineId) => {
            if (routineId) {
                console.log(routineId)
                navigate(`/routines/`);
            }
        });
    };
        return (
            <div>
                <Form className="routineForm">
                    <h2 className="routineForm">New Routine</h2>
    
                    <FormGroup>
                    <Label for="poseDropdown">Select a Pose:</Label>
                    <Input
                        className="routine-input"
                        type="select"
                        name="pose"
                        id="poseDropdown"
                        value={newRoutine.poseId}
                        onChange={(event) => {
                            const copy = { ...newRoutine }
                            copy.poseId = parseInt(event.target.value)
                            updateRoutine(copy)
                        }}
                    >
                        <option value="">Select...</option>
                        {poses.map((poses) => (
                            <option key={poses.id} value={poses.id}>{poses.name}</option>
                        ))}
                    </Input>
                </FormGroup>
                    <FormGroup>
                        <div className="form-group">
                            <Label htmlFor="intention">Intention</Label>
                            <Input id="intention" type="text" className="form-control"
                                value={
                                    newRoutine.intention
                                }
                                onChange={
                                    (event) => {
                                        const copy = {
                                            ...newRoutine
                                        }
                                        copy.intention = event.target.value
                                        updateRoutine(copy)
                                    }
                                }/>
                        </div>
                </FormGroup>
                <FormGroup>
                    <div className="form-group">
                        <Label htmlFor="cycles">Cycles</Label>
                        <Input id="cycle" type="text" className="form-control"
                            value={
                                newRoutine.cycles
                            }
                            onChange={
                                (event) => {
                                    const copy = {
                                        ...newRoutine
                                    }
                                    copy.cycles = event.target.value
                                    updateRoutine(copy)
                                }
                            }/>
                    </div>
            </FormGroup>
    
    
        <Button className="btn btn-primary"
            onClick={
                (clickEvent) => handleSaveButtonClick(clickEvent)
        }>
            Save Routine</Button>
    </Form>
    </div>
        )
    
    
    }
