import { useContext, useEffect, useState } from "react"
import { editComment, getCommentById } from "../Managers/CommentManager.js";
import { useNavigate, useParams, Link } from "react-router-dom";
import { Button, FormGroup, Input, Label } from "reactstrap";

export const CommentEdit = () => {

    const localSoulFlyUser = localStorage.getItem("users")
    const soulFlyUserObject = JSON.parse(localSoulFlyUser)

    const { commentId } = useParams();

    const navigate = useNavigate();
    
    const [editedComment, setEditedComment] = useState({
        text: "",
        content: "",
        usersId: soulFlyUserObject.id,
        createDateTime: Date.now()
    })
    useEffect(() => {
        getCommentById(commentId).then((res) => {
            setEditedComment(res)
        }
        );
    }, [commentId])
    if (!editedComment) {
        return null;
    }

    const handleSaveButtonClick = (e) => {
        e.preventDefault()

        const commentToEdit = {
            Id: parseInt(commentId),
            Text: editedComment.text,
            CreateDateTime: editedComment.createDateTime,
            UsersId: editedComment.usersId,
            RoutineId: editedComment.routineId

        }
        console.log(commentId)
        return editComment(commentToEdit)
            .then(() => {
                navigate(`/comments/${editedComment.routineId}`)
            })
    }
    

    return (
        <form className="comment-form">
            <h2 className="comment-form-title">Edit your Comment</h2>
                <FormGroup className="form-group">
                    <Label htmlFor="text">text:</Label>
                    <Input
                        className="comment-input"
                        type="text"
                        id="text"
                        value={editedComment.text}
                        onChange={
                            (event) => {
                                const copy = { ...editedComment }
                                copy.text = event.target.value
                                setEditedComment(copy)
                            }
                        } />
                </FormGroup>


            <Button onClick={(clickEvent) => handleSaveButtonClick(clickEvent)} className="btn btn-primary">Save Comment</Button>
            <Button tag={Link} to={`/comments/${editedComment.routineId}`} className="comment-btn">Cancel</Button>
        </form>
    );
}