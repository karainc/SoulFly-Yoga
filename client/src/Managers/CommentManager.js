const baseUrl = '/api/Comment';

export const getCommentsByRoutineId = (id) => {
    //make sure the fetch call matches the Request URL from your swagger
    return fetch(`${baseUrl}/${id}`)
        .then((res) => res.json())
};

export const addComment = (commentObject) => {
    return fetch(baseUrl, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(commentObject),
    })
    .then((res) => {
        if (!res.ok) {
            throw new Error("Failed to create new Comment")
        }
        return res.json();
    });
};


export const deleteComment = (commentId) => {
    return fetch(`/api/Comment/${commentId}`, {
      method: "DELETE",
    });
  };



  export const editComment = (comment) => {
 
    return fetch(`${baseUrl}/${comment.Id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(comment)
    })
}


export const getCommentById = (id) => {
    return fetch(`${baseUrl}/commentById/${id}`)
        .then((res) => res.json())
};


