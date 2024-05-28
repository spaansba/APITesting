-- !!Inserting data into a unique column!!

-- If you dont want to return a value:
-- Checks if the data value is already found within the column (conflict) do nothing
INSERT INTO tag(tag_name)
VALUES(@TagName)
ON CONFLICT (tag_name) DO NOTHING

-- If you DO want to return a value:
-- Updates the tag rows name with the EXCLUDED name (excluded = the row that coudnt be included because of conflict
-- Updating basically nothing but this allows us to return the id of the conflicting row
INSERT INTO tag (tag_name)
VALUES (@TagName)
ON CONFLICT(tag_name)
    DO UPDATE SET
    tag_name=EXCLUDED.tag_name
           RETURNING tag_id;


-- Typical way to do an UPSERT ("insert if it doesn't already exist, update if it does")
-- If there is no conflict, insert as normal
-- If there is a conflict update the current row
INSERT INTO latest_sensor_data(sensor_id, minimum, maximum, sum)
VALUES(@SensorId, @SensorValue, @SensorValue, @SensorValue)
    ON CONFLICT(sensor_id) 
    DO UPDATE SET
    minimum = LEAST(latest_sensor_data.minimum, EXCLUDED.minimum)
    maximum = GREATEST(latest_sensor_data.minimum, EXCLUDED.minimum)
    sum = latest_sensor_data.sum + EXCLUDED.sum