import React, { useState } from 'react';
import { Chip, TextField, Autocomplete } from '@material-ui/core';

const TagsComponent = ({ initialTags = [] }) => {
    const [tags, setTags] = useState(initialTags);
    const allTags = ['OpenAI', 'Natural Language Processing', 'Image Generation', 'Claude', 'Gemini']; // Example tags, could be fetched from a server

    const handleAddTag = (event, newValue) => {
        setTags([...new Set([...tags, ...newValue])]); // Avoid duplicate tags
    };

    const handleDeleteTag = (tagToDelete) => () => {
        setTags(tags.filter((tag) => tag !== tagToDelete));
    };

    return (
        <Autocomplete
            multiple
            id="tags-filled"
            options={allTags.map((option) => option)}
            defaultValue={initialTags}
            freeSolo
            onChange={handleAddTag}
            renderTags={(value, getTagProps) =>
                value.map((option, index) => (
                    <Chip variant="outlined" label={option} {...getTagProps({ index })} onDelete={handleDeleteTag(option)} />
                ))
            }
            renderInput={(params) => (
                <TextField {...params} variant="filled" label="Tags" placeholder="Add Tags" />
            )}
        />
    );
};

export default TagsComponent;
